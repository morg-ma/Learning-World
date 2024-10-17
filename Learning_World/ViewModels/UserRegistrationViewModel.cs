﻿namespace Learning_World.ViewModels
{
    public class UserRegistrationViewModel
    {
        public string CourseName { get; set; }
        public string Name { get; set; }
        public string PaymentMethod { get; set; } // CreditCard, PayPal, BankTransfer

        // Credit Card Information
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVC { get; set; }
    }


}
