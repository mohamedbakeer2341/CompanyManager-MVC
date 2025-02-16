using Company.Demo.BLL.Interfaces;
using Company.Demo.DAL.Data.Contexts;
using Company.Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAll()

        {
            if(typeof(T) == typeof(Employee))
                return (IEnumerable<T>)  await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T?> Get(int Id)
        {
            if (typeof(T) == typeof(Employee))
                return (T)(object) await _dbContext.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == Id);
            return await _dbContext.Set<T>().FindAsync(Id);    
        }
        public async Task Add(T entity)
        {
            //_dbContext.Set<T>().Add(entity);
            await _dbContext.AddAsync(entity);
        }
        public void Update(T entity)
        {
            //_dbContext.Set<T>().Update(entity);
             _dbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            //_dbContext.Set<T>().Remove(entity);
            _dbContext.Remove(entity);
        }
    }
}
