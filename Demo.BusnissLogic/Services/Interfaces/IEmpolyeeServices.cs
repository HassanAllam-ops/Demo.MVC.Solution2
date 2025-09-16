using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusnissLogic.DataTransferObjects.Empolyees;

namespace Demo.BusnissLogic.Services.Interfaces
{
    public interface IEmpolyeeServices
    {
        IEnumerable<EmpolyeeDto> GetAllEmpolyees(bool withTracking = false);
        EmpolyeeDetailsDto? GetEmpolyeeById(int id);
        int CreateEmpolyee(CreatedEmpolyeeDto empolyeeDto);
        int UpdateEmpolyee(UpdatedEmpolyeeDto empolyeeDto);
        bool DeleteEmpolyee(int id);
    }
}
