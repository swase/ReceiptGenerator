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
        //public int ItemTotal { get; set; }
        
        [Index(IsUnique =false)]
        public int ProductID { get; set; }
        public virtual ICollection<Product> Product { get; set; }

        public int? OrderID { get; set; }
        public Order Order { get; set; }
    }
}