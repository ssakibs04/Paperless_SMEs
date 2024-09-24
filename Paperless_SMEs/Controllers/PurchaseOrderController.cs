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
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly SMEContext _context;

        public PurchaseOrdersController(SMEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PurchaseOrder>> GetPurchaseOrders()
        {
            return _context.PurchaseOrders.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<PurchaseOrder> GetPurchaseOrder(int id)
        {
            var purchaseOrder = _context.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
                return NotFound();
            return purchaseOrder;
        }

        [HttpPost]
        public ActionResult<PurchaseOrder> CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPurchaseOrder), new { id = purchaseOrder.Id }, purchaseOrder);
        }

        [HttpPut("{id}")]
        public ActionResult<PurchaseOrder> UpdatePurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
                return BadRequest();

            _context.Entry(purchaseOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PurchaseOrders.Any(po => po.Id == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePurchaseOrder(int id)
        {
            var purchaseOrder = _context.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
                return NotFound();

            _context.PurchaseOrders.Remove(purchaseOrder);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
