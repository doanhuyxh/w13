using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marketplace
{
    [Table("PlayGames")]
    public class PlayGames
    {
        [Key]
        public long Id { get; set; }

        public int IdAccount { get; set; }

        public long SessionId { get; set; }

        public string Value { get; set; }
        public string ValueString { get; set; }
        public string Result { get; set; }
        public string ResultString { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal Amount { get; set; }

        public decimal AmountReceive { get; set; }
        public DateTime CompletedDate { get; set; }

        public int Status { get; set; }

    }
    public class PlayGamesHistory : PlayGames
    {
        public string Username { get; set; }
        public int  TotalRow { get; set; }
    }
}
