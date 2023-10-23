using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDCO2R_HFT_2023241.Models
{
    [Table("Games")]
    public class BoardGames
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BoardGameId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
