using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.Business.Mapping.AutoMapper;
using Yaman.Blog.BackEnd.Business.Services;
using Yaman.Blog.BackEnd.Business.Tools.JWTTool;
using Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.AppUserValidator;
using Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.BlogValidator;
using Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.CategoryValidator;
using Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.CommentValidator;
using Yaman.Blog.BackEnd.DataAcess.Context;
using Yaman.Blog.BackEnd.DataAcess.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.Repositories;
using Yaman.Blog.BackEnd.DataAcess.UnitOfWork;
using Yaman.Blog.BackEnd.Dtos.AppUserDtos;
using Yaman.Blog.BackEnd.Dtos.BlogDtos;
using Yaman.Blog.BackEnd.Dtos.CategoryDtos;
using Yaman.Blog.BackEnd.Dtos.CommentDtos;

namespace Yaman.Blog.BackEnd.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            });

            var profile = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new AppUserProfile());
                opt.AddProfile(new BlogProfile());
                opt.AddProfile(new CategoryProfile());
                opt.AddProfile(new CommentProfile());
            });
            var mapper = profile.CreateMapper();
            services.AddSingleton(mapper);


            services.AddScoped<IUow, Uow>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IJwtService, JwtService>();


            services.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();
            services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();
            services.AddTransient<IValidator<BlogCreateDto>, BlogCreateDtoValidator>();
            services.AddTransient<IValidator<BlogUpdateDto>, BlogUpdateDtoValidator>();
            services.AddTransient<IValidator<CategoryCreateDto>, CategoryCreateDtoValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateDtoValidator>();
            services.AddTransient<IValidator<CommentCreateDto>, CommentCreateDtoValidator>();
            services.AddTransient<IValidator<CommentUpdateDto>, CommentUpdateDtoValidator>();
        }
    }
}
