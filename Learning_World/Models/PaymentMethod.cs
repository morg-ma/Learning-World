using System.ComponentModel.DataAnnotations;

namespace Learning_World.Models
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodID { get; set; }
        public string Country { get; set; } 
        public string PaymentType { get; set; } // 'CreditCard', 'PayPal', etc.
        public string? CardName { get; set; } // Nullable for PayPal
        public string? CardNumber { get; set; } // Nullable for PayPal
        public string? ExpiryDate { get; set; } // Format: MM/YYYY
        public string? CVC { get; set; } // Nullable for PayPal
        public string? PayPalEmail { get; set; } // Nullable for Credit Card
    }
}
