using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WDCO2R_HFT_2023241.Models
{
    [Table("Games")]
    public class BoardGames
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BoardGameId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Rental> Rental { get; set; }
        public BoardGames()
        {

        }
        public override string ToString()
        {
            return $"BoardGameId : {BoardGameId}" +
                   $"\nTitle : {Title}" +
                   $"\nType: {Type}\n";
        }
    }
}
