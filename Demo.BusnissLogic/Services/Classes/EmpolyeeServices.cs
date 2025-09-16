using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusnissLogic.DataTransferObjects.Empolyees;
using Demo.BusnissLogic.Factories.Empolyees;
using Demo.BusnissLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Empolyees;

namespace Demo.BusnissLogic.Services.Classes
{
    public class EmpolyeeServices : IEmpolyeeServices
    {
        private readonly IEmpolyeeRepository _empolyeeRepository;

        public EmpolyeeServices(IEmpolyeeRepository empolyeeRepository)
        {
            _empolyeeRepository = empolyeeRepository;
        }

        public IEnumerable<EmpolyeeDto> GetAllEmpolyees(bool withTracking = false)
        {
            var empolyees = _empolyeeRepository.GetAll(withTracking);
            var empolyeeToReturn = empolyees.Select(e => new EmpolyeeDto());
            return empolyeeToReturn;

        }

        public EmpolyeeDetailsDto? GetEmpolyeeById(int id)
        {
            var empolyee = _empolyeeRepository.GetById(id);
            
            return empolyee is null ? null : empolyee.ToEmpolyeeDetailsDto();

        }


        public int CreateEmpolyee(CreatedEmpolyeeDto empolyeeDto)
        {
            return _empolyeeRepository.Add(empolyeeDto.ToEntity());
        }

        public int UpdateEmpolyee(UpdatedEmpolyeeDto empolyeeDto)
        {
           return _empolyeeRepository.Update(empolyeeDto.ToEntity());
        }

        public bool DeleteEmpolyee(int id)
        {
            var empolyee = _empolyeeRepository.GetById(id);
            if (empolyee is null)
            {
                return false;
            }
            else
            {
                var Result = _empolyeeRepository.Remove(empolyee);
                return Result > 0 ? true : false;
            }

        }

    }
}
