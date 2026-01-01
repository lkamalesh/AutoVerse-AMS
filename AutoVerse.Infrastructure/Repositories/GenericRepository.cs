using AutoVerse.Core.Interfaces.Repositories;
using AutoVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoVerse.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();// dynamically maps to the table.
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.AsNoTracking().ToListAsync();// AsNoTracking improves performance for read-only operations.
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            //T is a reference type (like Vehicle, Brand, VehicleType) which returns a object.
            return await _dbset.FindAsync(id);
        }

        public async Task AddAsync(T entiity)
        {
            await _dbset.AddAsync(entiity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entiity)
        {
            _dbset.Update(entiity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbset.FindAsync(id);

            if (entity != null)
            {
                _dbset.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
