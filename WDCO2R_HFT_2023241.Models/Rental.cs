using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WDCO2R_HFT_2023241.Models
{
    [Table("Rental")]
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey(nameof(Models.BoardGames))]
        public int BoardGameId { get; set; }
        [ForeignKey(nameof(Models.Customer))]
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [Range (0, 80000)]
        public double Price { get; set; }
        [Required]
        [Range(0,730)]
        public double TimeLeft { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual BoardGames BoardGame { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual Customer Customer { get; set; }

        public override string ToString()
        {
            return $"RentId: {RentId}" +
                  $"\nBoardGameId :  {BoardGameId}" +
                  $"\nCustomerId : {CustomerId}" +
                  $"\nPrice : {Price}Ft " +
                  $"\nTimeLeft: {TimeLeft} Day\n";
        }
    }
}
