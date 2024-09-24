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
    public class QuotationLineItemsController : ControllerBase
    {
        private readonly SMEContext _context;

        public QuotationLineItemsController(SMEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuotationLineItem>> GetQuotationLineItems()
        {
            return _context.QuotationLineItems.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<QuotationLineItem> GetQuotationLineItem(int id)
        {
            var lineItem = _context.QuotationLineItems.Find(id);
            if (lineItem == null)
                return NotFound();
            return lineItem;
        }

        [HttpPost]
        public ActionResult<QuotationLineItem> CreateQuotationLineItem(QuotationLineItem lineItem)
        {
            _context.QuotationLineItems.Add(lineItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetQuotationLineItem), new { id = lineItem.Id }, lineItem);
        }

        [HttpPut("{id}")]
        public ActionResult<QuotationLineItem> UpdateQuotationLineItem(int id, QuotationLineItem lineItem)
        {
            if (id != lineItem.Id)
                return BadRequest();

            _context.Entry(lineItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.QuotationLineItems.Any(q => q.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteQuotationLineItem(int id)
        {
            var lineItem = _context.QuotationLineItems.Find(id);
            if (lineItem == null)
                return NotFound();

            _context.QuotationLineItems.Remove(lineItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
