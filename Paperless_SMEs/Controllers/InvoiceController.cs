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
    public class InvoicesController : ControllerBase
    {
        private readonly SMEContext _context;

        public InvoicesController(SMEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Invoice>> GetInvoices()
        {
            return _context.Invoices.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoice(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
                return NotFound();
            return invoice;
        }

        [HttpPost]
        public ActionResult<Invoice> CreateInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceId }, invoice);
        }

          [HttpPut("{id}")]
        public ActionResult<Invoice> UpdateInvoice(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
                return BadRequest();

            _context.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Invoices.Any(i => i.InvoiceId == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteInvoice(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
                return NotFound();

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            return NoContent();
        }
    }
}