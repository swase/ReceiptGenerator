using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public partial class OrderItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }

        public OrderItem()
        {
            SetTotal();
        }

        public void SetTotal()
        {
            Total = Quantity * UnitPrice;
        }
    }
}
