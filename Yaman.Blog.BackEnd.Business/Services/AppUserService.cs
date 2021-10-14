using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.UnitOfWork;
using Yaman.Blog.BackEnd.Dtos.AppUserDtos;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.Business.Services
{
    public class AppUserService : Services<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<AppUser> CheckUserAsync(AppUserLoginDto dto)
        {
            return await _uow.GetRepository<AppUser>().GetByFilterAsync(I => I.UserName == dto.UserName && I.Password == dto.Password);
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _uow.GetRepository<AppUser>().GetByFilterAsync(I => I.UserName == userName);
        }
    }
}
