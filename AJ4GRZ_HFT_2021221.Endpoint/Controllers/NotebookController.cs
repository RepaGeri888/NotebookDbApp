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
    public class NotebookController : ControllerBase
    {
        readonly INotebookLogic nl;
        IHubContext<SignalRHub> hub;

        public NotebookController(INotebookLogic nl, IHubContext<SignalRHub> hub)
        {
            this.nl = nl;
            this.hub = hub;
        }

        // GET: /notebook
        [HttpGet]
        public IEnumerable<Notebook> Get()
        {
            return nl.ReadAll();
        }

        // GET /notebook/5
        [HttpGet("{id}")]
        public Notebook Get(int id)
        {
            return nl.Read(id);
        }

        // POST /notebook
        [HttpPost]
        public void Post([FromBody] Notebook value)
        {
            nl.Create(value);
            this.hub.Clients.All.SendAsync("NotebookCreated", value);
        }

        // PUT /notebook
        [HttpPut]
        public void Put([FromBody] Notebook value)
        {
            nl.Update(value);
            this.hub.Clients.All.SendAsync("NotebookUpdated", value);
        }

        // DELETE /notebook/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var notebookToDelete = this.nl.Read(id);
            nl.Delete(id);
            this.hub.Clients.All.SendAsync("NotebookDeleted", notebookToDelete);
        }
    }
}
