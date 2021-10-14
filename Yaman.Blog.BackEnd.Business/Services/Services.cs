using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Extensions;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.Common;
using Yaman.Blog.BackEnd.DataAcess.UnitOfWork;
using Yaman.Blog.BackEnd.Dtos.Interfaces;
using Yaman.Blog.BackEnd.Entities.Concrete;
using Yaman.Blog.BackEnd.Entities.Interfaces;

namespace Yaman.Blog.BackEnd.Business.Services
{
    public class Services<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseTable
    {

        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDtoValidator;
        private readonly IValidator<UpdateDto> _updateDtoValidator;
        private readonly IUow _uow;

        public Services(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                await _uow.GetRepository<T>().CreateAsync(_mapper.Map<T>(dto));
                await _uow.SaveChangeAsync();
                return new Response<CreateDto>(ResponseType.Success, dto);
            }
            return new Response<CreateDto>(dto, errors: result.ConvertToCustomValidationError());
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync();
            var dto = _mapper.Map<List<ListDto>>(data);
            return new Response<List<ListDto>>(ResponseType.Success, dto);
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilterAsync(I => I.Id == id);
            if(data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye sahip data bulunamadı");
            }
            var dto = _mapper.Map<IDto>(data);
            return new Response<IDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _uow.GetRepository<T>().FindByIdAsync(id);
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye sahip data bulunamadı");
            }
            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChangeAsync();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if(result.IsValid)
            {
                var unchangedData = await _uow.GetRepository<T>().FindByIdAsync(dto.Id);
                if(unchangedData == null)
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"{dto.Id} idsine sahip data bulunamadı");
                }
                var entity = _mapper.Map<T>(dto);
                _uow.GetRepository<T>().Update(entity, unchangedData);
                await _uow.SaveChangeAsync();
                return new Response<UpdateDto>(ResponseType.Success, dto);
            }
            return new Response<UpdateDto>(dto, result.ConvertToCustomValidationError());
        }
    }
}
