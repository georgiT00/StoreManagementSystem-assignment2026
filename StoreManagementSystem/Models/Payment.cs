namespace StoreManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public PaymentType Type { get; set; }
    }
}
