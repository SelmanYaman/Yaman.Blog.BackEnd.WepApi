using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.DataAcess.Context;
using Yaman.Blog.BackEnd.DataAcess.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.Repositories;
using Yaman.Blog.BackEnd.Entities.Concrete;
using Yaman.Blog.BackEnd.Entities.Interfaces;

namespace Yaman.Blog.BackEnd.DataAcess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly BlogContext _blogContext;

        public Uow(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task SaveChangeAsync()
        {
            await _blogContext.SaveChangesAsync();
        }

        public IRepository<T> GetRepository<T>() where T : BaseTable
        {
            return new Repository<T>(_blogContext);
        }
    }
}
