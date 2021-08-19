using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantData
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public double Subtotal { get; set; }
        public int CustomerID { get; set; } 
        //public int ProductID { get; set; }

        
        //public virtual ICollection<Product> Product { get; set; }
    }
}
