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
    public class CpuController : ControllerBase
    {
        readonly ICpuLogic cl;
        IHubContext<SignalRHub> hub;

        public CpuController(ICpuLogic cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
        }

        // GET: /cpu
        [HttpGet]
        public IEnumerable<Cpu> Get()
        {
            return cl.ReadAll();
        }

        // GET /cpu/5
        [HttpGet("{id}")]
        public Cpu Get(int id)
        {
            return cl.Read(id);
        }

        // POST /cpu
        [HttpPost]
        public void Post([FromBody] Cpu value)
        {
            cl.Create(value);
            this.hub.Clients.All.SendAsync("CpuCreated", value);
        }

        // PUT /cpu
        [HttpPut]
        public void Put([FromBody] Cpu value)
        {
            cl.Update(value);
            this.hub.Clients.All.SendAsync("CpuUpdated", value);
        }

        // DELETE /cpu/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cpuToDelete = this.cl.Read(id);
            cl.Delete(id);
            this.hub.Clients.All.SendAsync("CpuDeleted", cpuToDelete);
        }
    }
}
