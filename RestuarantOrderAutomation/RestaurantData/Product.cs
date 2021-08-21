using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantData
{
    public class Product
    {
        //[ForeignKey("OrderDetail")]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } = 0.0;

        public OrderDetail OrderDetail { get; set; }
    }
}
