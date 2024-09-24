using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class Quotation
    {
        public Quotation()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            QuotationLineItems = new HashSet<QuotationLineItem>();
        }

        public int Id { get; set; }
        public DateTime QuotationDate { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<QuotationLineItem> QuotationLineItems { get; set; }
    }
}
