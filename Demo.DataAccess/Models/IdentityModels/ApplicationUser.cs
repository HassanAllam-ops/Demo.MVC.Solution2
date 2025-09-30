using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demo.DataAccess.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="FName")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "LName")]
        public string? LastName { get; set; }
        
    }
}
