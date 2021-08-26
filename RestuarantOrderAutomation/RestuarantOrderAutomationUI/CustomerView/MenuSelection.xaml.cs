using BusinessLayer;
using RestaurantData;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestuarantOrderAutomationUI.CustomerView
{
    /// <summary>
    /// Interaction logic for MenuSelection.xaml
    /// </summary>
    public partial class MenuSelection : Window
    {
        private CustomerUILogic _custLogic = new CustomerUILogic();
        public MenuSelection()
        {
            InitializeComponent();
            lbCustomerOrderList.ItemsSource = _custLogic.CurrentOrder;
            lbProductSelection.ItemsSource = _custLogic.Products;
            SubtotalLabel.DataContext = _custLogic;
        }

        private void btnAddProductToOrder_Click(object sender, RoutedEventArgs e)
        {
            _custLogic.AddToCurrentOrder((Product)lbProductSelection.SelectedItem);
            lbCustomerOrderList.Items.Refresh();
        }

        private void RemoveFromOrderList_Click(object sender, RoutedEventArgs e)
        {
            _custLogic.RemoveOrderItem((OrderItem)lbCustomerOrderList.SelectedItem);
            lbCustomerOrderList.Items.Refresh();
        }

        private void ReduceQuantity_Click(object sender, RoutedEventArgs e)
        {
            _custLogic.ReduceItemQuantity((OrderItem)lbCustomerOrderList.SelectedItem);
            lbCustomerOrderList.Items.Refresh();
        }

        private void AddQuantity_Click(object sender, RoutedEventArgs e)
        {
            _custLogic.IncreaseItemQuantity((OrderItem)lbCustomerOrderList.SelectedItem);
            lbCustomerOrderList.Items.Refresh();

        }

        private void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_custLogic.CurrentOrder.Count != 0)
            {
                CustOrderConfirmation.IsOpen = true;
            }
        }

        private void Not_Ready_Click(object sender, RoutedEventArgs e)
        {
            CustOrderConfirmation.IsOpen = false;
        }

        private void Confirm_Order_Click(object sender, RoutedEventArgs e)
        {
            CustOrderConfirmation.IsOpen = false;
            _custLogic.ProcessCurrentOrder();
            this.Close();
            
        }
    }
}
