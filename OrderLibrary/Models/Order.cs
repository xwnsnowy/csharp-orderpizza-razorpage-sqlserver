using System;
using System.Collections.Generic;

namespace OrderLibrary.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string? Phone { get; set; }
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? Total { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
