using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTEcommerce.Web.Models
{
    public class PlayGamePost
    {
        public string value { get; set; }
        public decimal price { get; set; }
        public int sessionId { get; set; }
    }
    public class WithdrawalModel
    {
        public int BankId { get; set; }
        public string BankAccount { get; set; }
        public string BankNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}