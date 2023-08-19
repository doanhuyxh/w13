using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    [Table("AccountCustomer")]
    public class AccountCustomer
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal AmountAvaiable { get; set; }

        public bool IsActive { get; set; }
        public string Token { get; set; }
        public int BankId { get; set; }
        public string BankAccount { get; set; }
        public string BankNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }

    }
    public class AccountCustomerExtend : AccountCustomer
    {
        public int TotalRow { get; set; }
    }
    public class AccountUpdate
    {
        [Required]
        public int BankId { get; set; }
        [Required]
        public string BankAccount { get; set; }
        [Required]
        public string BankNumber { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
    }
    public class AccountCustomerRegister
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }
    }
}
