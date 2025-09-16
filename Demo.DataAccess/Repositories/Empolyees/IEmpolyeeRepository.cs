using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models.Empolyees;
using Demo.DataAccess.Repositories.Genarics;

namespace Demo.DataAccess.Repositories.Empolyees
{
    public interface IEmpolyeeRepository : IGenaricRepostiroy<Empolyee>
    {
        object Add(Demo.BusnissLogic.DataTransferObjects.Empolyees.CreatedEmpolyeeDto createdEmpolyeeDto);
    }
}
