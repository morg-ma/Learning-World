using Learning_World.Models;
using System.ComponentModel;

namespace Learning_World.ViewModels
{
    public class UserRegistrationViewModel
    {
        //public Course Course { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal CoursePrice { get; set; }
        [DisplayName("Username")]
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; }
        // Credit Card Information
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVC { get; set; }   
    }


}
