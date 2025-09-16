using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.Departments;
using Demo.DataAccess.Repositories.Genarics;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Departments
{
    public class DepartmentRepositories : GenaricRepository<Department>,IDepartmentRepositories
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepositories(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
