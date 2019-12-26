using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetAsync(int id);

        Task<List<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);

        Task SetStatusAsync(int id, bool status);
    }
}
