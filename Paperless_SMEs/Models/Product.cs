using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class Product
    {
        public Product()
        {
            PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
            QuotationLineItems = new HashSet<QuotationLineItem>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public virtual ICollection<QuotationLineItem> QuotationLineItems { get; set; }
    }
}
