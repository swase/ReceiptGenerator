using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantData
{
    public class OrderDetail
    {
        [Key]
        public int OrderItemID { get; set; }
        //public double PriceWithDiscount { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}