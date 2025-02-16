using Company.Demo.BLL.Interfaces;
using Company.Demo.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            DepartmentRepository = new DepartmentRepository(dbContext);
            EmployeeRepository = new EmployeeRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
