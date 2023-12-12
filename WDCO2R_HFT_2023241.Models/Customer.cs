using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDCO2R_HFT_2023241.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Required]
        [Range(18,99)]
        public int CustomerAge { get; set; }
        [NotMapped]
        public virtual ICollection<Rental> Rental { get; set; }


        public override string ToString()
        {
            return $"CustomerId: {CustomerId}" +
                   $"\nCustomerName :  {CustomerName}" +
                   $"\nCustomerAge : {CustomerAge}\n";
        }
    }
}
