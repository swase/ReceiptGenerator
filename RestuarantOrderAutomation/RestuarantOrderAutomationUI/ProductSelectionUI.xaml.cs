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
using RestaurantData;
using BusinessLayer;
using System.Data;

namespace RestuarantOrderAutomationUI
{
    
    public partial class ProductSelectionUI : Window
    {
        private CustomerUILogic _custLogic = new CustomerUILogic();
        public ProductSelectionUI()
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
    }


}
