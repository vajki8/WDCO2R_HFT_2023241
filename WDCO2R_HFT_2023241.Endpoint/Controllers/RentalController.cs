using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using WDCO2R_HFT_2023241.Endpoint.Services;
using WDCO2R_HFT_2023241.Logic.InterFaces;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        IRentalLogic logic;
        IHubContext<SignalRHub> hub;

        public RentalController(IRentalLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpPost]
        public void Create([FromBody] Rental value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("RentalCreated", value);
        }
        [HttpGet]
        public IEnumerable<Rental> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Rental Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Update([FromBody] Rental value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RentalUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var rentToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RentalDeleted", rentToDelete);
        }
    }
}
