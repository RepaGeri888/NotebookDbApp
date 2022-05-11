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
    public class GpuController : ControllerBase
    {
        readonly IGpuLogic gl;
        IHubContext<SignalRHub> hub;

        public GpuController(IGpuLogic gl, IHubContext<SignalRHub> hub)
        {
            this.gl = gl;
            this.hub = hub;
        }

        // GET: /gpu
        [HttpGet]
        public IEnumerable<Gpu> Get()
        {
            return gl.ReadAll();
        }

        // GET /gpu/5
        [HttpGet("{id}")]
        public Gpu Get(int id)
        {
            return gl.Read(id);
        }

        // POST /gpu
        [HttpPost]
        public void Post([FromBody] Gpu value)
        {
            gl.Create(value);
            this.hub.Clients.All.SendAsync("GpuCreated", value);
        }

        // PUT /gpu
        [HttpPut]
        public void Put([FromBody] Gpu value)
        {
            gl.Update(value);
            this.hub.Clients.All.SendAsync("GpuUpdated", value);
        }

        // DELETE /gpu/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gpuToDelete = this.gl.Read(id);
            gl.Delete(id);
            this.hub.Clients.All.SendAsync("GpuDeleted", gpuToDelete);
        }
    }
}
