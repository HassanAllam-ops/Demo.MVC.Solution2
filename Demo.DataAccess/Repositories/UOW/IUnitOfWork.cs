using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Repositories.Departments;
using Demo.DataAccess.Repositories.Empolyees;

namespace Demo.DataAccess.Repositories.UOW
{
    public interface IUnitOfWork
    {
        public IEmpolyeeRepository EmpolyeeRepository { get; }
        public IDepartmentRepositories DepartmentRepositories { get; }
        public int SaveChanges();   
    }
}
