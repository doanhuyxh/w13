using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    [Table("SessionGames")]
    public class SessionGames
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Value { get; set; }
        public int Value2 { get; set; }

        public DateTime CompleteDate { get; set; }
    }

}
