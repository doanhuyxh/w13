using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    [Table("Settings")]
    public class Settings
    {
        [Key]
        public int Id { get; set; }
        public string Key_Setting { get; set; }
        public string Display_Name { get; set; }
        public string Value { get; set; }
    }
}
