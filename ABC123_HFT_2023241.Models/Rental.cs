using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDCO2R_HFT_2023241.Models
{
    [Table("Rental")]
    public class Rental
    {
        [ForeignKey(nameof(Models.BoardGames))]
        public int BoardGameId { get; set; }
        [ForeignKey(nameof(Models.Customer))]
        public int CustomerId { get; set; }

        [NotMapped]
        public virtual BoardGames BoardGame { get; set; }

        [NotMapped]
        public virtual Customer Customer { get; set; }
        [Key]
        public int RentId { get; set; }
        public string RentType { get; set; }

        public DateTime StartofRental { get; set; }
        public DateTime EndofRental { get; set; }
    }
}
