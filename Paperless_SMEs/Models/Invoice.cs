using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Payments = new HashSet<Payment>();
        }

        public int InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public int DeliveryChallanId { get; set; }

        public virtual DeliveryChallan DeliveryChallan { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
