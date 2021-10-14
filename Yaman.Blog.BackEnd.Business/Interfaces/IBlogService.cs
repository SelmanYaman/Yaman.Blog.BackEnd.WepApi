using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.BlogDtos;

namespace Yaman.Blog.BackEnd.Business.Interfaces
{
    public interface IBlogService : IService<BlogCreateDto, BlogUpdateDto, BlogListDto, Entities.Concrete.Blog>
    {
        Task<List<BlogListDto>> GetLastFiveAsync();
        Task<List<BlogListDto>> GetAllByCategoriesIdAsync(int categoryId);
        Task<List<BlogListDto>> SearchAsync(string s);
    }
}
