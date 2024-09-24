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
    public class DeliveryChallansController : ControllerBase
    {
        private readonly SMEContext _context;

        public DeliveryChallansController(SMEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DeliveryChallan>> GetDeliveryChallans()
        {
            return _context.DeliveryChallans.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<DeliveryChallan> GetDeliveryChallan(int id)
        {
            var deliveryChallan = _context.DeliveryChallans.Find(id);
            if (deliveryChallan == null)
                return NotFound();
            return deliveryChallan;
        }

        [HttpPost]
        public ActionResult<DeliveryChallan> CreateDeliveryChallan(DeliveryChallan deliveryChallan)
        {
            _context.DeliveryChallans.Add(deliveryChallan);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDeliveryChallan), new { id = deliveryChallan.DeliveryChallanId }, deliveryChallan);
        }

        [HttpPut("{id}")]
        public ActionResult<DeliveryChallan> UpdateDeliveryChallan(int id, DeliveryChallan deliveryChallan)
        {
            if (id != deliveryChallan.DeliveryChallanId)
                return BadRequest();

            _context.Entry(deliveryChallan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DeliveryChallans.Any(dc => dc.DeliveryChallanId == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDeliveryChallan(int id)
        {
            var deliveryChallan = _context.DeliveryChallans.Find(id);
            if (deliveryChallan == null)
                return NotFound();

            _context.DeliveryChallans.Remove(deliveryChallan);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
