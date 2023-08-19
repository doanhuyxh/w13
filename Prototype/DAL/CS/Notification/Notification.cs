using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
    }
}
