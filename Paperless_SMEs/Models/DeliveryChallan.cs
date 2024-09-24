using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class DeliveryChallan
    {
        public DeliveryChallan()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int DeliveryChallanId { get; set; }
        public DateTime Date { get; set; }
        public string ChallanNumber { get; set; } = null!;
        public int PurchaseOrderId { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
