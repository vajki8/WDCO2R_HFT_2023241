using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WDCO2R_HFT_2023241.Logic.InterFaces;

namespace WDCO2R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IRentalLogic logic;

        public StatController(IRentalLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<object> OlderCustomer()
        {
            return this.logic.OlderCustomer();
        }
        [HttpGet]
        public IEnumerable<object> typeFamily()
        {
            return this.logic.typeFamily();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, string>> currentCustomer()
        {
            return this.logic.currentCustomers();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> HighestTime()
        {
            return this.logic.HighestTime();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> FreePrice()
        {
            return this.logic.FreePrice();
        }

        [HttpGet]
        public IEnumerable<object> withinWeek()
        {
            return this.logic.Withinweek();
        }

    }
}
