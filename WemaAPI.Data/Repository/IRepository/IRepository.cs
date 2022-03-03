using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WemaAPI.Data.Repository.IRepository
{
    /// <summary>
    /// Base Repository Interface
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null, string includeProperties = null);

        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        Task AddAsync(T entity);

        void Remove(T entity);

        Task UpdateAsync(T entity);

    }
}
