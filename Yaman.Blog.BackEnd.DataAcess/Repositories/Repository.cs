using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.DataAcess.Context;
using Yaman.Blog.BackEnd.DataAcess.Interfaces;
using Yaman.Blog.BackEnd.Entities.Concrete;
using Yaman.Blog.BackEnd.Entities.Interfaces;

namespace Yaman.Blog.BackEnd.DataAcess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseTable
    {
        private readonly BlogContext _blogContext;

        public Repository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _blogContext.Set<T>().AddAsync(entity);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _blogContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _blogContext.Set<T>().OrderByDescending(I => I.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _blogContext.Set<T>().Where(filter).OrderByDescending(I => I.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            return await _blogContext.Set<T>().OrderByDescending(keySelector).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector)
        {
            return await _blogContext.Set<T>().Where(filter).OrderByDescending(keySelector).AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _blogContext.Set<T>().SingleOrDefaultAsync(filter);
        }

        public IQueryable<T> GetQuery()
        {
            return _blogContext.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            _blogContext.Set<T>().Remove(entity);
        }

        public void Update(T entity, T unchanged)
        {
            _blogContext.Entry(unchanged).CurrentValues.SetValues(entity);
        }
    }
}
