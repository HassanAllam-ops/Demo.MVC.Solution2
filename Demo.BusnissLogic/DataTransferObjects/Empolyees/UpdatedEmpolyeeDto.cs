using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models.Empolyees;

namespace Demo.BusnissLogic.DataTransferObjects.Empolyees
{
    public class UpdatedEmpolyeeDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name Sould be Less than 50 Char")]
        [MinLength(3, ErrorMessage = "Name Should be at Least 3 Char")]
        public string Name { get; set; } = null!;
        [Range(24, 40)]
        public int? Age { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmpolyeeType EmpolyeeType { get; set; }
    }
}
