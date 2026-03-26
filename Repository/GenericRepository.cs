using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
       protected readonly RestaurantContext _context;
       protected readonly DbSet<T> _dbset;
       public GenericRepository(RestaurantContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

       public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

       public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.FirstOrDefaultAsync(predicate);
        }

       public void Remove(T entity)
        {
            _dbset.Remove(entity);
                
        }

       public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }
    }  
}