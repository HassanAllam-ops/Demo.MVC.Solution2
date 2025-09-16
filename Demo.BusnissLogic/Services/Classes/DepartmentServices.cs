using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusnissLogic.DataTransferObjects.Departments;
using Demo.BusnissLogic.Factories.Departments;
using Demo.BusnissLogic.Services.Interfaces;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Departments;

namespace Demo.BusnissLogic.Services.Classes
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepositories _departmentRepositories;

        public DepartmentServices(IDepartmentRepositories departmentRepositories)
        {
            _departmentRepositories = departmentRepositories;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var department = _departmentRepositories.GetAll();
            var departmentsToReturn = department.Select(D => D.ToDepartmentDto());
            return departmentsToReturn;
            //return department;
        }

        public DepartmentsDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepositories.GetById(id);
            ///if(department is null)
            ///{
            ///    return null;
            ///}
            ///else
            ///{
            ///    return new DepartmentsDetailsDto()
            ///    {
            ///        Id = department.Id,
            ///        Name = department.Name,
            ///        Code = department.Code,
            ///        Description = department.Description,
            ///        CreatedBy = department.CreatedBy,
            ///        LastModifiedBy = department.LastModfiedBy,
            ///        DateofCreation = DateOnly.FromDateTime(department.CreatedOn),
            ///        IsDeleted = department.IsDeleted,
            ///    };
            ///}
            return department == null ? null : department.ToDepartmentsDetailsDto();

        }

        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            return _departmentRepositories.Add(departmentDto.ToEntity());
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            return _departmentRepositories.Update(departmentDto.ToEntity());
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepositories.GetById(id);
            if (department is null)
            {
                return false;
            }
            else
            {
                var Result = _departmentRepositories.Remove(department);
                return Result > 0 ? true : false;
            }

        }

    }
}
