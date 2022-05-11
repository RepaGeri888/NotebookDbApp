using AJ4GRZ_HFT_2021221.Logic;
using AJ4GRZ_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotebookDbApp.Endpoint.Services;
using System.Collections.Generic;

namespace AJ4GRZ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        IBrandLogic bl;
        IHubContext<SignalRHub> hub;

        public BrandController(IBrandLogic bl, IHubContext<SignalRHub> hub)
        {
            this.bl = bl;
            this.hub = hub;
        }

        // GET: /brand
        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return bl.ReadAll();
        }

        // GET /brand/5
        [HttpGet("{id}")]
        public Brand Get(int id)
        {
            return bl.Read(id);
        }

        // POST /brand
        [HttpPost]
        public void Post([FromBody] Brand value)
        {
            bl.Create(value);
            this.hub.Clients.All.SendAsync("BrandCreated", value);
        }

        // PUT /brand
        [HttpPut]
        public void Put([FromBody] Brand value)
        {
            bl.Update(value);
            this.hub.Clients.All.SendAsync("BrandUpdated", value);
        }

        // DELETE /brand/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brandToDelete = this.bl.Read(id);
            bl.Delete(id);
            this.hub.Clients.All.SendAsync("BrandDeleted", brandToDelete);
        }
    }
}
