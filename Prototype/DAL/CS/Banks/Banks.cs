using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    [Table("Banks")]
    public class Banks
    {
        [Key]
        public int Id { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
        public string NapasCodeBank { get; set; }
        public string NapasBankName { get; set; }
        public string NapasBankId { get; set; }
    }
}
