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
    public class QuotationsController : ControllerBase
    {
        private readonly SMEContext _context;

        public QuotationsController(SMEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Quotation>> GetQuotations()
        {
            return _context.Quotations.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Quotation> GetQuotation(int id)
        {
            var quotation = _context.Quotations.Find(id);
            if (quotation == null)
                return NotFound();
            return quotation;
        }

        [HttpPost]
        public ActionResult<Quotation> CreateQuotation(Quotation quotation)
        {
            _context.Quotations.Add(quotation);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetQuotation), new { id = quotation.Id }, quotation);
        }

        [HttpPut("{id}")]
        public ActionResult<Quotation> UpdateQuotation(int id, Quotation quotation)
        {
            if (id != quotation.Id)
                return BadRequest();

            _context.Entry(quotation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quotations.Any(q => q.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteQuotation(int id)
        {
            var quotation = _context.Quotations.Find(id);
            if (quotation == null)
                return NotFound();

            _context.Quotations.Remove(quotation);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
