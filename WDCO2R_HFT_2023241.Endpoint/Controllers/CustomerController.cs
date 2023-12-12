using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WDCO2R_HFT_2023241.Logic.InterFaces;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerLogic logic;

        public CustomerController(ICustomerLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void Create([FromBody] Customer value)
        {
            this.logic.Create(value);
        }
        [HttpGet]
        public IEnumerable<Customer> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Customer Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPut]
        public void Update([FromBody] Customer value)
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

