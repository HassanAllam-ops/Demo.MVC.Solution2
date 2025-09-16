using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusnissLogic.DataTransferObjects.Departments;
using Demo.BusnissLogic.DataTransferObjects.Empolyees;
using Demo.DataAccess.Models.Departments;
using Demo.DataAccess.Models.Empolyees;

namespace Demo.BusnissLogic.Factories.Empolyees
{
    public static class EmpolyeeFactory
    {
        public static EmpolyeeDetailsDto ToEmpolyeeDetailsDto(this Empolyee empolyee)
        {
            return new EmpolyeeDetailsDto()
            {
                Id = empolyee.Id,
                Name = empolyee.Name,
                Age = empolyee.Age,
                Salary = empolyee.Salary,
                IsActive = empolyee.IsActive,
                Email = empolyee.Email,
                PhoneNumber = empolyee.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(empolyee.HiringDate),
                Gender = empolyee.Gender.ToString(),
                EmpolyeeType = empolyee.EmpolyeeType.ToString(),
                CreatedBy = empolyee.CreatedBy,
                CreatedOn = empolyee.CreatedOn,
                LastModfiedBy = empolyee.LastModfiedBy,
                LastModifiedOn = empolyee.LastModifiedOn
            };
        }
        public static EmpolyeeDto ToEmpolyeeDto(this Empolyee empolyee)
        {
            return new EmpolyeeDto()
            {
                Id = empolyee.Id,
                Name = empolyee.Name,
                Age = empolyee.Age,
                Salary = empolyee.Salary,
                IsActive = empolyee.IsActive,
                Email = empolyee.Email,
                Gender = empolyee.Gender.ToString(),
                EmpolyeeType = empolyee.EmpolyeeType.ToString()
            };
        }
        public static Empolyee ToEntity(this CreatedEmpolyeeDto empolyeeDto)
        {
            return new Empolyee()
            {
                Name = empolyeeDto.Name,
                Age = empolyeeDto.Age,
                Address = empolyeeDto.Address ?? string.Empty,
                Salary = empolyeeDto.Salary,
                Email = empolyeeDto.Email ?? string.Empty,
                PhoneNumber = empolyeeDto.PhoneNumber ?? string.Empty,
                HiringDate = empolyeeDto.HiringDate.ToDateTime(new TimeOnly()),
                IsActive = empolyeeDto.IsActive,
                Gender = empolyeeDto.Gender,
                EmpolyeeType = empolyeeDto.EmpolyeeType
            };
        }
        public static Empolyee ToEntity(this UpdatedEmpolyeeDto empolyeeDto)
        {
            return new Empolyee()
            {
                Name = empolyeeDto.Name,
                Age = empolyeeDto.Age,
                Address = empolyeeDto.Address ?? string.Empty,
                Salary = empolyeeDto.Salary,
                Email = empolyeeDto.Email ?? string.Empty,
                PhoneNumber = empolyeeDto.PhoneNumber ?? string.Empty,
                HiringDate = empolyeeDto.HiringDate.ToDateTime(new TimeOnly()),
                IsActive = empolyeeDto.IsActive,
                Gender = empolyeeDto.Gender,
                EmpolyeeType = empolyeeDto.EmpolyeeType
            };
        }
    }
}
