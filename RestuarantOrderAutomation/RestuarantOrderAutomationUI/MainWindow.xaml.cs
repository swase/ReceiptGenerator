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
using BusinessLayer;
using RestuarantOrderAutomationUI.CustomerView;
using RestuarantOrderAutomationUI.WorkersView;

namespace RestuarantOrderAutomationUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var orders = new OrderProcessing();
            orders.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string pName = ProductName.Text;
            //double price = Convert.ToDouble(ProductPrice.Text);
            //ProductManager pm = new ProductManager();
            //pm.Create(pName, price);
        }

        private void start_new_order_click(object sender, RoutedEventArgs e)
        {
            var menu = new MenuSelection();
            menu.Show();
            //this.Hide();
        }
    }
}
