using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WDCO2R_HFT_2023241.Logic.InterFaces;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        IBoardGameLogic logic;

        public BoardGameController(IBoardGameLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void Create([FromBody] BoardGames value)
        {
            this.logic.Create(value);
        }
        [HttpGet]
        public IEnumerable<BoardGames> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public BoardGames Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Update([FromBody] BoardGames value)
        {
            this.logic.Update(value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
