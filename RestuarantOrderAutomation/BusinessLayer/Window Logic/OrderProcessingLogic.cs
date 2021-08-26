using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantData;
using System.Timers;
using BusinessLayer.DataManager;

namespace BusinessLayer.Window_Logic
{
    public class OrderProcessingLogic : INotifyPropertyChanged
    {      

        public event PropertyChangedEventHandler PropertyChanged;
        private List<Order> _orders = new List<Order>();
        private List<CurrentOrderDetails> _currentOrderDetails = new List<CurrentOrderDetails>();
        public Order CurrentOrder = new Order();
        private Dictionary<OrderStatus, int> _orderSummary;

        public OrderProcessingLogic()
        {
            InitOrderSummary();
            UpdateOrderList();


        }

        public Dictionary<OrderStatus, int> OrderSummary
        {
            get { return _orderSummary; }
            set
            {
                _orderSummary = value;
                OnPropertyChanged(nameof(OrderSummary));
            }
        }


        public List<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public List<CurrentOrderDetails> CurrentOrderDetails
        {
            get { return _currentOrderDetails; }
            set
            {
                _currentOrderDetails = value;
                OnPropertyChanged(nameof(CurrentOrderDetails));
            }
        }


        //For observable items
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public void UpdateOrderList()
        {
            Orders = OrderManager.RetrieveAllActiveOrders();
            GetOrderStatusSummary();
        }


        public void SetCurrentOrder(Order order)
        {
            if (order != null)
            {
                CurrentOrder = order;
            }
        }

        public void SetCurrentOrderDetails()
        {
            var tempCurOrders = OrderDetailManager.GetOrderDetails(CurrentOrder.OrderID);
            if (tempCurOrders != null && tempCurOrders.Count > 0)
            {
                var tempList = new List<CurrentOrderDetails>();
                foreach (var item in tempCurOrders)
                {
                    tempList.Add(new CurrentOrderDetails
                    {
                        ProductName = ProductManager.GetProductName(item.ProductID),
                        Quantity = item.Quantity,
                    });
                }
                CurrentOrderDetails = tempList;
            }

        }

        public void ProcessOrder(OrderStatus newStatus)
        {
            var om = new OrderManager();
            om.Update(CurrentOrder.OrderID, CurrentOrder.Subtotal, newStatus);
            CurrentOrder.Status = newStatus;
            UpdateOrderList();
        }

        public void CancelCurrentOrder()
        {
            var om = new OrderManager();
            om.Delete(CurrentOrder.OrderID);
            UpdateOrderList();
        }

        public void GetOrderStatusSummary()
        {
            var om = new OrderManager();
            foreach(var item in OrderSummary)
            {
                OrderSummary[item.Key] = om.GetNumOrdersForStatus(item.Key);
            }
        }

        public void InitOrderSummary()
        {
            _orderSummary = new Dictionary<OrderStatus, int>()
                {
                { OrderStatus.OrderConfirmed, 0},
                { OrderStatus.Processing, 0},
                { OrderStatus.ReadyForCollection, 0}
                };
        }


    }

    public partial class CurrentOrderDetails
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }


}
