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
using Demo.DataAccess.Repositories.UOW;

namespace Demo.BusnissLogic.Services.Classes
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _umitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            _umitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var department = _umitOfWork.DepartmentRepositories.GetAll();
            var departmentsToReturn = department.Select(D => D.ToDepartmentDto());
            return departmentsToReturn;
            //return department;
        }

        public DepartmentsDetailsDto? GetDepartmentById(int id)
        {
            var department = _umitOfWork.DepartmentRepositories.GetById(id);
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
             _umitOfWork.DepartmentRepositories.Add(departmentDto.ToEntity());
            return _umitOfWork.SaveChanges();
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
           _umitOfWork.DepartmentRepositories.Update(departmentDto.ToEntity());
            return _umitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _umitOfWork.DepartmentRepositories.GetById(id);
            if (department is null)
            {
                return false;
            }
            else
            {
                _umitOfWork.DepartmentRepositories.Remove(department);
                var Result = _umitOfWork.SaveChanges();
                return Result > 0 ? true : false;
            }

        }

    }
}
