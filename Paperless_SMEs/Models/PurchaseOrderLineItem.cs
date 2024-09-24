﻿using System;
using System.Collections.Generic;

namespace Paperless_SMEs.Models
{
    public partial class PurchaseOrderLineItem
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
    }
}
