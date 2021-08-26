using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataManager;
using RestaurantData;
using System.ComponentModel;

namespace BusinessLayer
{
    public class CustomerUILogic : INotifyPropertyChanged
    {
        private ProductManager _productManager = new ProductManager();
        private int _orderID = OrderManager.Create(0, OrderStatus.Ordering);
        public List<OrderItem> CurrentOrder { get; set; }

        public List<Product> Products { get; set; }
        private double _subtotal = 0;

        public double Subtotal 
        {
            get { return _subtotal; }
            set
            {
                _subtotal = value;
                OnPropertyChanged(nameof(Subtotal));

            }
        }

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
            CalculateSubtotal();
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            var query = CurrentOrder.Where(i => i.ProductName == orderItem.ProductName).FirstOrDefault();
            CurrentOrder.Remove(query);
            CalculateSubtotal();
        }

        public void ReduceItemQuantity(OrderItem orderItem)
        {
            if (orderItem != null)
            {
                var query = CurrentOrder.Where(i => i.ProductName == orderItem.ProductName).FirstOrDefault();
                if (query != null && query.Quantity == 1)
                {
                    CurrentOrder.Remove(query);
                    query.SetTotal();
                }
                else
                {
                    orderItem.Quantity--;
                    query.SetTotal();
                }
                CalculateSubtotal();
            }
        }

        public void IncreaseItemQuantity(OrderItem orderItem)
        {
            var query = CurrentOrder.Where(i => i.ProductName == orderItem.ProductName).FirstOrDefault();

            query.Quantity++;
            query.SetTotal();
            CalculateSubtotal();
        }

        public void CalculateSubtotal()
        {
            double tempTotal = 0;
            foreach (var item in CurrentOrder)
            {
                tempTotal += item.UnitPrice * item.Quantity;

            }
            Subtotal = tempTotal;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ProcessCurrentOrder()
        {
            var orderManager = new OrderManager();
            var orderDetailManager = new OrderDetailManager();
            orderManager.Update(_orderID, Subtotal, OrderStatus.OrderConfirmed);

            foreach (var item in CurrentOrder)
            {
                int prodID = ProductManager.GetProductID(item.ProductName);
                orderDetailManager.Create(prodID, item.Quantity, _orderID);
            }
        }

    }


}
