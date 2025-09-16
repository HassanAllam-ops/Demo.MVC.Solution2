using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusnissLogic.DataTransferObjects.Departments
{
    public class DepartmentsDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateOnly DateofCreation { get; set; }
        public int LastModifiedBy { get; set; }
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
