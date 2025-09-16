using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusnissLogic.DataTransferObjects.Departments;
using Demo.DataAccess.Models.Departments;

namespace Demo.BusnissLogic.Factories.Departments
{
    internal static class DepartmentFactory
    {
        public static DepartmentsDetailsDto ToDepartmentsDetailsDto(this Department department)
        {
            return new DepartmentsDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModfiedBy,
                DateofCreation = DateOnly.FromDateTime(department.CreatedOn),
                IsDeleted = department.IsDeleted,
            };

        }
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateofCreation = DateOnly.FromDateTime(department.CreatedOn)
            };
        }
        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description ?? string.Empty,
                CreatedOn = departmentDto.DateofCreation.ToDateTime(new TimeOnly())
            };
        }
        public static Department ToEntity(this UpdateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Discription,
                CreatedOn = departmentDto.DateofCreation.ToDateTime(new TimeOnly()) 
            };
        }
    }
}
