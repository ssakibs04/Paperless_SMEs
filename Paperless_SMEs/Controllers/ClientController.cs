using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Paperless_SMEs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paperless_SMEs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly SMEContext _context;

        public ClientsController(SMEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return _context.Clients.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
                return NotFound();
            return client;
        }

        [HttpPost]
        public ActionResult<Client> CreateClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public ActionResult<Client> UpdateClient(int id, Client client)
        {
            if (id != client.Id)
                return BadRequest();

            _context.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clients.Any(c => c.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
                return NotFound();

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
