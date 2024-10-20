using System.ComponentModel.DataAnnotations;

namespace Learning_World.ViewModels
{
    public class RegistrationViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
