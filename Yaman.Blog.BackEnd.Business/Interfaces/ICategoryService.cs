using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.CategoryDtos;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.Business.Interfaces
{
    public interface ICategoryService : IService<CategoryCreateDto,CategoryUpdateDto,CategoryListDto,Category>
    {
    }
}
