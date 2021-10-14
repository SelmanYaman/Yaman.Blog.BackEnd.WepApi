using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.UnitOfWork;
using Yaman.Blog.BackEnd.Dtos.CategoryDtos;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.Business.Services
{
    public class CategoryService : Services<CategoryCreateDto, CategoryUpdateDto, CategoryListDto, Category>, ICategoryService
    {
        public CategoryService(IMapper mapper, IValidator<CategoryCreateDto> createDtoValidator, IValidator<CategoryUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
        }
    }
}
