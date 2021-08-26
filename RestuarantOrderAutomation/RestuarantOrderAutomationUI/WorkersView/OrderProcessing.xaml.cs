using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLayer.Window_Logic;
using RestaurantData;
using System.Timers;

namespace RestuarantOrderAutomationUI.WorkersView
{
    /// <summary>
    /// Interaction logic for OrderProcessing.xaml
    /// </summary>
    public partial class OrderProcessing : Window
    {


        private OrderProcessingLogic _opLogic = new OrderProcessingLogic();
        public OrderProcessing()
        {
            InitializeComponent();
            lbOrders.ItemsSource = _opLogic.Orders;
            lvShowOrderDetails.ItemsSource = _opLogic.CurrentOrderDetails;
            ShowOrderSummary();


        }

        private void btnShowOrder_Click(object sender, RoutedEventArgs e)
        {
            Order selectedOrder = (Order)lbOrders.SelectedItem;
            _opLogic.SetCurrentOrder(selectedOrder);
            _opLogic.SetCurrentOrderDetails();
            lbOrders.DataContext = _opLogic.Orders;
            OrderProcessingSubtotal.Content = $"£{selectedOrder.Subtotal}";
            lvShowOrderDetails.ItemsSource = _opLogic.CurrentOrderDetails;
            lvShowOrderDetails.Items.Refresh();
        }


        private void OnImageButtonClick(object sender, RoutedEventArgs e)
        {
            _opLogic.UpdateOrderList();
            lbOrders.ItemsSource = _opLogic.Orders;
            lbOrders.Items.Refresh();
            ShowOrderSummary();
        }

        private void btnProcessOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_opLogic.CurrentOrderDetails != null && _opLogic.CurrentOrderDetails.Count != 0)
            {
                ProcessOrderWindow.IsOpen = true;
                DisableOrderProcessBtns();

                switch (_opLogic.CurrentOrder.Status)
                {
                    case (OrderStatus.OrderConfirmed):
                        btnPrepareOrder.IsEnabled = true;
                        break;
                    case (OrderStatus.Processing):
                        btnReadyForCollection.IsEnabled = true;
                        break;
                    case (OrderStatus.ReadyForCollection):
                        btnCompleteOrder.IsEnabled = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnExitOrderProcessPopUp_Click(object sender, RoutedEventArgs e)
        {
            ProcessOrderWindow.IsOpen = false;

        }

        private void btnReadyForCollection_Click(object sender, RoutedEventArgs e)
        {
            ProcessOrderWindow.IsOpen = false;
            _opLogic.ProcessOrder(OrderStatus.ReadyForCollection);
            lbOrders.ItemsSource = _opLogic.Orders;
            lbOrders.Items.Refresh();
            ShowOrderSummary();

        }

        private void btnPrepareOrder_Click(object sender, RoutedEventArgs e)
        {
            ProcessOrderWindow.IsOpen = false;
            _opLogic.ProcessOrder(OrderStatus.Processing);
            lbOrders.ItemsSource = _opLogic.Orders;
            lbOrders.Items.Refresh();
            ShowOrderSummary();


        }

        private void btnCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            ProcessOrderWindow.IsOpen = false;
            _opLogic.ProcessOrder(OrderStatus.Collected);
            lbOrders.ItemsSource = _opLogic.Orders;
            lbOrders.Items.Refresh();
            ShowOrderSummary();

        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            _opLogic.CancelCurrentOrder();
            ProcessOrderWindow.IsOpen = false;
            lbOrders.ItemsSource = _opLogic.Orders;
            lbOrders.Items.Refresh();
            lvShowOrderDetails.Items.Refresh();
            ShowOrderSummary();

        }

        private void DisableOrderProcessBtns()
        {
            btnPrepareOrder.IsEnabled = false;
            btnCompleteOrder.IsEnabled = false;
            btnReadyForCollection.IsEnabled = false;
            lbOrders.ItemsSource = _opLogic.Orders;
            lbOrders.Items.Refresh();
        }

        public void ShowOrderSummary()
        {
            TotOrdConfirmed.Content = _opLogic.OrderSummary[OrderStatus.OrderConfirmed];
            TotOrdProcessing.Content = _opLogic.OrderSummary[OrderStatus.Processing];
            TotOrdReadForCollection.Content = _opLogic.OrderSummary[OrderStatus.ReadyForCollection];
        }
    }
}
