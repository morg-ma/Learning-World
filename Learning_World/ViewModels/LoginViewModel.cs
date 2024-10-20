using System.ComponentModel.DataAnnotations;

namespace Learning_World.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public String Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remeber Me")]
        public bool Remember { get; set; }
    }
}
