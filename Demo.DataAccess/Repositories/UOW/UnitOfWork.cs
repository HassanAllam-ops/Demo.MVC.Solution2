using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Departments;
using Demo.DataAccess.Repositories.Empolyees;

namespace Demo.DataAccess.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy <IDepartmentRepositories> _departmentRepositories;
        private Lazy <IEmpolyeeRepository> _empolyeeRepository;
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _departmentRepositories = new Lazy<IDepartmentRepositories>(() => new DepartmentRepositories(_dbContext));
            _empolyeeRepository = new Lazy<IEmpolyeeRepository>(() => new EmpolyeeRepository(_dbContext));
        } 
        public IEmpolyeeRepository EmpolyeeRepository => _empolyeeRepository.Value;

        public IDepartmentRepositories DepartmentRepositories => _departmentRepositories.Value;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
