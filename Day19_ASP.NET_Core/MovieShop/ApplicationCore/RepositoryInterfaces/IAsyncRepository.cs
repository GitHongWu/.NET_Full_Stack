using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    // common CRUD operations
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetById(int Id);
        Task<IEnumerable<T>> ListAll();
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> filter);
        Task<int> GetCount(Expression<Func<T, bool>> filter);
        Task<bool> GetExists(Expression<Func<T, bool>> filter);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> ListAllWithIncludesAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes);
    }
}
