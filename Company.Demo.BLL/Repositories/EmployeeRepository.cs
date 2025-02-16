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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {

        }

        public async Task<IEnumerable<Employee>> GetByName(string name)
        {
            return await _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.Department).ToListAsync();
        }
    }
}
