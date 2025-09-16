using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.Empolyees;
using Demo.DataAccess.Repositories.Genarics;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Empolyees
{
    public class EmpolyeeRepository : GenaricRepository<Empolyee>,IEmpolyeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmpolyeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
           _dbContext = dbContext;
        }

        

    }
}
