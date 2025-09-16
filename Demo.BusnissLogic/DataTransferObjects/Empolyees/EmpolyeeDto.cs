using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusnissLogic.DataTransferObjects.Empolyees
{
    public class EmpolyeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string Gender { get; set; } = null!;
        [Display(Name = "Empolyee Type")]
        public string EmpolyeeType { get; set; } = null!;

    }
}
