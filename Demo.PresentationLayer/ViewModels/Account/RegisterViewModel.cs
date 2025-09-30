using System.ComponentModel.DataAnnotations;

namespace Demo.PresentationLayer.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name Is Required")]
        [MaxLength(50, ErrorMessage = "First Name Max Length Is 50 Characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        [MaxLength(50, ErrorMessage = "Last Name Max Length Is 50 Characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Name Is Required")]
        [MaxLength(50, ErrorMessage = "User Name Max Length Is 50 Characters")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
