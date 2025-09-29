using Microsoft.AspNetCore.Mvc;

namespace Questionnaire.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();       // Read all
        Task<T?> GetByIdAsync(int id);            // Read by ID
        Task AddAsync(T entity);                  // Create
        Task<T> UpdateAsync(T entity);               // Update
        Task DeleteAsync(T entity);                   // Delete      
    }
}
