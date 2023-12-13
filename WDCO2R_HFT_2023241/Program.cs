using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Repository;

namespace WDCO2R_HFT_2023241.Client
{
    class Program
       
    {
        static void Main(string[] args)
        {
            RentalDbContext ctx = new RentalDbContext();

            foreach (var item in ctx.BoardGame)
            {
                Console.WriteLine(item.Title + ": " + item.Type);
            }
        }
    }
}
