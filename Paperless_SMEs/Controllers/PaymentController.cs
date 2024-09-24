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
    public class PaymentsController : ControllerBase
    {
        private readonly SMEContext _context;

        public PaymentsController(SMEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            return _context.Payments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
                return NotFound();
            return payment;
        }

        [HttpPost]
        public ActionResult<Payment> CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public ActionResult<Payment> UpdatePayment(int id, Payment payment)
        {
            if (id != payment.PaymentId)
                return BadRequest();

            _context.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Payments.Any(p => p.PaymentId == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
