using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class Client
    {
        public Client()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Quotations = new HashSet<Quotation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
