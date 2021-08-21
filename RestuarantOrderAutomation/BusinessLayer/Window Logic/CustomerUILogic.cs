using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataManager;
using RestaurantData;

namespace BusinessLayer
{
    public class CustomerUILogic
    {
        private ProductManager _productManager = new ProductManager();
        private int _orderID = OrderManager.Create(0, "ordering");
        public List<OrderItem> CurrentOrder { get; set; }

        public List<Product> Products { get; set; }
        public CustomerUILogic()
        {
            Products = _productManager.RetrieveAll();
            CurrentOrder = new List<OrderItem>();
        }

        public void AddToCurrentOrder(Product product)
        {
            var pm = new ProductManager();
            OrderItem orderItem = _productManager.RetrieveForCustSelection(product);
            var query = CurrentOrder.Where(co => co.ProductName == product.ProductName).FirstOrDefault();
            if (CurrentOrder == null || query == null)
            {
                CurrentOrder.Add(orderItem);
            }
            else
            {
                query.Quantity++;
                query.SetTotal(); 
                
            }
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            var query = CurrentOrder.Where(i => i.ProductName == orderItem.ProductName).FirstOrDefault();
            CurrentOrder.Remove(query);
        }

        public void ReduceItemQuantity(OrderItem orderItem)
        {
            var query = CurrentOrder.Where(i => i.ProductName == orderItem.ProductName).FirstOrDefault();
            if (query.Quantity == 1)
            {
                CurrentOrder.Remove(query);
                query.SetTotal();

            }
            else
            {
                orderItem.Quantity--;
                query.SetTotal();
            }
        }

        public void IncreaseItemQuantity(OrderItem orderItem)
        {
            var query = CurrentOrder.Where(i => i.ProductName == orderItem.ProductName).FirstOrDefault();

            query.Quantity++;
            query.SetTotal();

        }

    }


}
