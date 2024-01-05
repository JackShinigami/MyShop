using DTO_MyShop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {

        Order ReturnOrder = new Order();

        public EditOrderWindow(Order order)
        {

            InitializeComponent();
            ReturnOrder = order.Clone() as Order;

            // Load Customer
            var customers = BUS_MyShop.BUS_Customers.Instance.GetAllCustomers();
            customerComboBox.ItemsSource = customers;
            customerComboBox.SelectedIndex = customers.IndexOf(ReturnOrder.Customer);

            // Load Product
            var products = BUS_MyShop.BUS_Products.Instance.GetAllProducts();
            productComboBox.ItemsSource = products;
            addProductButton.IsEnabled = false;

            // Load Order
            var orderDetails = BUS_MyShop.BUS_OrderDetails.Instance.GetOrderDetailsByOrderId(order.Id);
            orderDetailDataGrid.ItemsSource = orderDetails;

            this.DataContext = ReturnOrder;
        }

        private void orderDetailDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            removeProductButton.IsEnabled = true;
        }


        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnOrder.CustomerId = (customerComboBox.SelectedItem as Customer).Id;
            ReturnOrder.OrderDate = DateTime.Now;

            try
            {

                // xoá các order detail cũ
                BUS_MyShop.BUS_OrderDetails.Instance.DeleteOrderDetailsByOrderId(ReturnOrder.Id);
                // thêm các order detail mới
                foreach (OrderDetail orderDetail in orderDetailDataGrid.Items)
                {
                    int quantity = orderDetail.Quantity!.Value;
                    BUS_MyShop.BUS_OrderDetails.Instance.AddOrderDetail(ReturnOrder.Id, orderDetail.ProductId, quantity);
                }

            } catch (Exception ex)
            {
                MessageWindow.Show(ex.Message, "Lỗi");
                return;
            }

            this.Close();
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            var product = productComboBox.SelectedItem as Product;
            if(product == null)
            {
                MessageWindow.Show("Please select a product");
                return;
            }
            int quantity = 0;
            try
            {

                quantity = int.Parse(countTextBox.Text);

            } catch (Exception ex)
            {
                MessageWindow.Show("Vui lòng nhập số lượng");
                countTextBox.Text = "";
                countTextBox.Focus();
                return;
            }
            orderDetailDataGrid.Items.Add(new OrderDetail()
            {
                ProductId = product.Id,
                Product = product,
                Quantity = 1
            });
        }

        private void removeProductButton_Click(object sender, RoutedEventArgs e)
        {
            orderDetailDataGrid.Items.Remove(orderDetailDataGrid.SelectedItem);
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void productComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addProductButton.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
