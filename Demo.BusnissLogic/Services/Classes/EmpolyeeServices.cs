using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusnissLogic.DataTransferObjects.Empolyees;
using Demo.BusnissLogic.Factories.Empolyees;
using Demo.BusnissLogic.Services.Interfaces;
using Demo.DataAccess.Models.Empolyees;
using Demo.DataAccess.Repositories.Empolyees;
using Demo.DataAccess.Repositories.UOW;

namespace Demo.BusnissLogic.Services.Classes
{
    public class EmpolyeeServices : IEmpolyeeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        public EmpolyeeServices(IUnitOfWork unitOfWork,
                                          IMapper mapper,
                                          IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           _attachmentService = attachmentService;
        }

        public IEnumerable<EmpolyeeDto> GetAllEmpolyees(string? EmployeeSearchName, bool withTracking = false)
        {

            #region GetEnumrable,GetQueryable
            //var empolyees = _empolyeeRepository.GetEnumrable();
            //var empolyeeToReturn = empolyees.Select(E => new EmpolyeeDto
            //{
            //    Name = E.Name,
            //    Age = E.Age,
            //    Email = E.Email,
            //});
            // Select All Empolyee Properties

            // var empolyees = _empolyeeRepository.GetQueryable();
            // var empolyeeToReturn = empolyees.Select(E => new EmpolyeeDto
            // {
            //     Name = E.Name,
            //     Age = E.Age,
            //     Email = E.Email,
            // });
            // Select Specific Empolyee Properties
            #endregion

            IEnumerable<Empolyee> empolyees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                 empolyees = _unitOfWork.EmpolyeeRepository.GetAll(withTracking);
            }
            else
            {
                 empolyees = _unitOfWork.EmpolyeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            }

            var empolyeeToReturn = _mapper.Map<IEnumerable<Empolyee>, IEnumerable<EmpolyeeDto>>(empolyees);
            return empolyeeToReturn;

            ///var empolyees = _empolyeeRepository.GetAll(E => new EmpolyeeDto()
            ///{
            ///    Id = E.Id,
            ///    Name = E.Name,
            ///    Age = E.Age,
            ///    Salary = E.Salary,
            ///}).Where(E => E.Age > 24);
            ///return empolyees;

            ///var empolyeeToReturn = empolyees.Select(e => new EmpolyeeDto());
            ///return empolyeeToReturn;

        }

        public EmpolyeeDetailsDto? GetEmpolyeeById(int id)
        {
            var empolyee = _unitOfWork.EmpolyeeRepository.GetById(id);
            return empolyee is null ? null : _mapper.Map<Empolyee,EmpolyeeDetailsDto>(empolyee);

            //return empolyee is null ? null : empolyee.ToEmpolyeeDetailsDto();

        }


        public int CreateEmpolyee(CreatedEmpolyeeDto empolyeeDto)
        {
            var empolyee = _mapper.Map<CreatedEmpolyeeDto, Empolyee>(empolyeeDto);
            if (empolyeeDto.Image is not null)
            {
               empolyee.ImageName = _attachmentService.Upload(empolyeeDto.Image, "Images");
            }
             _unitOfWork.EmpolyeeRepository.Add(empolyee);
            return _unitOfWork.SaveChanges();
        }

        public int UpdateEmpolyee(UpdatedEmpolyeeDto empolyeeDto)
        {
            var empolyee = _mapper.Map<UpdatedEmpolyeeDto, Empolyee>(empolyeeDto);
            if (empolyeeDto.Image is not null)
            {
                if (!string.IsNullOrWhiteSpace(empolyee.ImageName))
                {
                    _attachmentService.Delete(empolyee.ImageName);
                }
                empolyee.ImageName = _attachmentService.Upload(empolyeeDto.Image, "Images");
            }
            _unitOfWork.EmpolyeeRepository.Update(empolyee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmpolyee(int id)
        {
            var empolyee = _unitOfWork.EmpolyeeRepository.GetById(id);
            if (empolyee is null)
            {
                return false;
            }
            empolyee.IsDeleted = true;
            _unitOfWork.EmpolyeeRepository.Update(empolyee);
            var result =  _unitOfWork.SaveChanges();
            if (result > 0) return true;
            else return false;
            

            /// Hard Delete
            ///else
            ///{
            ///    var Result = _empolyeeRepository.Remove(empolyee);
            ///    return Result > 0 ? true : false;
            ///}

        }

    }
}
