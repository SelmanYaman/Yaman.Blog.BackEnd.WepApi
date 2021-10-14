using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.UnitOfWork;
using Yaman.Blog.BackEnd.Dtos.BlogDtos;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.Business.Services
{
    public class BlogService : Services<BlogCreateDto, BlogUpdateDto, BlogListDto, Entities.Concrete.Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public BlogService(IMapper mapper, IValidator<BlogCreateDto> createDtoValidator, IValidator<BlogUpdateDto> updateDtoValidator, IUow uow, IBlogRepository blogRepository) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _uow = uow;
        }

        public async Task<List<BlogListDto>> GetAllByCategoriesIdAsync(int categoryId)
        {
            //return _mapper.Map<List<BlogListDto>>(await _blogRepository.GetAllByCategoriesIdAsync(categoryId));
            return _mapper.Map<List<BlogListDto>>(await _uow.GetRepository<Entities.Concrete.Blog>().GetAllAsync(I => I.CategoryId == categoryId));
        }

        public async Task<List<BlogListDto>> GetLastFiveAsync()
        {
            //return _mapper.Map<List<BlogListDto>>(await _blogRepository.GetLastFiveAsync());
            return _mapper.Map<List<BlogListDto>>(await _uow.GetRepository<Entities.Concrete.Blog>().GetAllAsync()).Take(5).ToList();
        }

        public async Task<List<BlogListDto>> SearchAsync(string s)
        {
            return _mapper.Map<List<BlogListDto>>(await _uow.GetRepository<Entities.Concrete.Blog>().GetAllAsync(I => I.Title.Contains(s) || I.ShortDescription.Contains(s) || I.Description.Contains(s)));
        }
    }
}
