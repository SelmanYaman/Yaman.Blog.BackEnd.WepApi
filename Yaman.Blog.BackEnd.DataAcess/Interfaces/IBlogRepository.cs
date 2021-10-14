using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaman.Blog.BackEnd.DataAcess.Interfaces
{
    public interface IBlogRepository : IRepository<Entities.Concrete.Blog>
    {
        //Task<List<Entities.Concrete.Blog>> GetLastFiveAsync();
        //Task<List<Entities.Concrete.Blog>> GetAllByCategoriesIdAsync(int categoryId);
    }
}
