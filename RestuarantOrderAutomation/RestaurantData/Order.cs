using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantData
{
    public class Order
    {
        //[ForeignKey("OrderDetail")]
        public int OrderID { get; set; }
        public double Subtotal { get; set; }
        //public Customer Customer { get; set; }
        public OrderStatus Status { get; set; }

        public int? OrderItemID { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }

    }

    public enum OrderStatus
    {
        Ordering,
        OrderConfirmed,
        Processing,
        ReadyForCollection,
        Collected
    };
}
