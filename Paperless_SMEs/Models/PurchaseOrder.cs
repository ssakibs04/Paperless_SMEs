using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            DeliveryChallans = new HashSet<DeliveryChallan>();
            PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
        }

        public int Id { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public int? ClientId { get; set; }
        public int QuotationId { get; set; }

        public virtual Client? Client { get; set; }
        public virtual Quotation Quotation { get; set; } = null!;
        public virtual ICollection<DeliveryChallan> DeliveryChallans { get; set; }
        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
    }
}
