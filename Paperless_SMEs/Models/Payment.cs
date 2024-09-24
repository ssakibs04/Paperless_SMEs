using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; } = null!;
    }
}
