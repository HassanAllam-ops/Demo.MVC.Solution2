using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Models.Departments
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public string Code { get; set; } = null!;
    }
}
