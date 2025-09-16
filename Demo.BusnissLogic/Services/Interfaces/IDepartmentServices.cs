using Demo.BusnissLogic.DataTransferObjects.Departments;

namespace Demo.BusnissLogic.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentsDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}