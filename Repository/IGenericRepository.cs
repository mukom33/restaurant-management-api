using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repository
{
    public interface IGenericRepository<T>where T:class 
    {
        Task<List<T>>GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T,bool>> predicate);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task SaveAsync();
    }
}