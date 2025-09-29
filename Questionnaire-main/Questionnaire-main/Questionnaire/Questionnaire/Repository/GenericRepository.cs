using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Data;
using Questionnaire.Model.Entity;
using Questionnaire.Repository.Interface;

namespace Questionnaire.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly QuestionnaireDBContext _context;
        public GenericRepository(QuestionnaireDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)   // POST
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)   //DELETE
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()  //GETALL
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)  //GETBYID
        {
            var user = await _context.Set<T>().FindAsync(id);
            return user;
        }

        public async Task<T> UpdateAsync(T entity)  //UPDATE
        {           
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }        
    }
}