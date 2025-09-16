using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusnissLogic.DataTransferObjects.Departments
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Discription { get; set; } = null!;
        public DateOnly DateofCreation { get; set; }
    }
}
