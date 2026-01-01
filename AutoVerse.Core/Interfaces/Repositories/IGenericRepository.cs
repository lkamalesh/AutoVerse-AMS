using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // <T> is a type parameter — it means this interface will only work with entities or models(of any kind).
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);// T? → Might return null if record is not there.
        Task AddAsync(T Entity);
        Task UpdateAsync(T Entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
