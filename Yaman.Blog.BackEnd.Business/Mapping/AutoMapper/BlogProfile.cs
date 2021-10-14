using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.BlogDtos;

namespace Yaman.Blog.BackEnd.Business.Mapping.AutoMapper
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogCreateDto, Entities.Concrete.Blog>().ReverseMap();
            CreateMap<BlogListDto, Entities.Concrete.Blog>().ReverseMap();
            CreateMap<BlogUpdateDto, Entities.Concrete.Blog>().ReverseMap();
        }
    }
}
