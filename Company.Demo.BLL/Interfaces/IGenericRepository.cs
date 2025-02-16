using Company.Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int Id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
