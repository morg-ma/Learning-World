namespace Learning_World.ViewModels
{
    public class UserRegistrationViewModel
    {
        public string CourseName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; }
        public string PaymentMethod { get; set; } // CreditCard, PayPal, BankTransfer
        // Credit Card Information
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVC { get; set; }
        public string PayPalEmail { get; set; }   
    }


}
