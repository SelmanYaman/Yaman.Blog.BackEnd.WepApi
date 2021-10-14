using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Entities.Concrete;
using Yaman.Blog.BackEnd.Entities.Interfaces;

namespace Yaman.Blog.BackEnd.DataAcess.Interfaces
{
    public interface IRepository<T> where T : BaseTable
    {
        //hepsini getir
        Task<List<T>> GetAllAsync();
        //where şartına göre getir
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        //sıralayarak getir
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> keySelector);
        //where şartına ve sıralamaya göre getir
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector);
        //idye göre getir
        Task<T> FindByIdAsync(int id);
        //filtreye göre getir.
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> GetQuery();
        void Remove(T entity);
        Task CreateAsync(T entity);
        void Update(T entity, T unchanged);
    }
}
