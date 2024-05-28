using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using WDCO2R_HFT_2023241.Endpoint.Services;
using WDCO2R_HFT_2023241.Logic.InterFaces;
using WDCO2R_HFT_2023241.Models;
using Newtonsoft.Json.Linq;

namespace WDCO2R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        IBoardGameLogic logic;
        IHubContext<SignalRHub> hub;

        public BoardGameController(IBoardGameLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpPost]
        public void Create([FromBody] BoardGame value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("GameCreated", value);
        }
        [HttpGet]
        public IEnumerable<BoardGame> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public BoardGame Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Update([FromBody] BoardGame value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("GameUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gameToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GameDeleted", gameToDelete);
        }
    }
}
