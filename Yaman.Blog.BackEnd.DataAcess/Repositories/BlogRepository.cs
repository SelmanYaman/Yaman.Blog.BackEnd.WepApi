using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.DataAcess.Context;
using Yaman.Blog.BackEnd.DataAcess.Interfaces;

namespace Yaman.Blog.BackEnd.DataAcess.Repositories
{
    public class BlogRepository : Repository<Entities.Concrete.Blog>, IBlogRepository
    {
        private readonly BlogContext _blogContext;
        public BlogRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        //public async Task<List<Entities.Concrete.Blog>> GetLastFiveAsync()
        //{
        //    return await _blogContext.Set<Entities.Concrete.Blog>().OrderByDescending(I => I.PostedTime).Take(5).ToListAsync();
        //} 
        
        //public async Task<List<Entities.Concrete.Blog>> GetAllByCategoriesIdAsync(int categoryId)
        //{
        //    return await _blogContext.Set<Entities.Concrete.Blog>().Where(I => I.CategoryId == categoryId).OrderByDescending(I => I.PostedTime).ToListAsync();
        //}
    }
}
