using System;
using System.Collections.Generic;

namespace OrderLibrary.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public decimal? BasePrice { get; set; }
        public string? Extras { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
