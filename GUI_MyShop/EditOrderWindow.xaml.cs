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

        public Order ReturnOrder = new Order();
        BindingList<OrderDetail> OrderDetails = new BindingList<OrderDetail>();
        BindingList<Customer> Customers = new BindingList<Customer>();
        BindingList<Product> Products = new BindingList<Product>();
        public bool EditMode = false;


        public EditOrderWindow(Order order, bool editMode = false)
        {

            InitializeComponent();
            ReturnOrder = order.Clone() as Order;
            EditMode = editMode;

            // Load Customer
            Customers = BUS_MyShop.BUS_Customers.Instance.GetAllCustomers();
            customerComboBox.Items.Clear();
            customerComboBox.ItemsSource = Customers;

            // Load Product
            Products = BUS_MyShop.BUS_Products.Instance.GetAllProducts();
            productComboBox.Items.Clear();
            productComboBox.ItemsSource = Products;
            addProductButton.IsEnabled = false;

            // Load Order
            OrderDetails = BUS_MyShop.BUS_OrderDetails.Instance.GetOrderDetailsByOrderId(order.Id);
            orderDetailDataGrid.ItemsSource = OrderDetails;



            if (editMode)
            {
                orderIDTextBox.IsEnabled = false;
                customerComboBox.IsEnabled = false;

                int index = Customers.IndexOf(Customers.FirstOrDefault(c => c.Id == order.CustomerId));
                customerComboBox.SelectedIndex = index;
            } else
            {
                Title = "Thêm đơn hàng";
                titleLabel.Content = "Thêm đơn hàng";
            }

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


            DialogResult = true;
            this.Close();
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            var product = productComboBox.SelectedItem as Product;
            if (product == null)
            {
                MessageWindow.Show("Vui lòng chọn sản phẩm");
                return;
            }
            int quantity = 0;
            try
            {

                quantity = int.Parse(countTextBox.Text);

            }
            catch (Exception ex)
            {
                MessageWindow.Show("Vui lòng nhập số lượng");
                countTextBox.Text = "";
                countTextBox.Focus();
                return;
            }


            OrderDetails.Add(new OrderDetail()
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity
            });
        }

        private void removeProductButton_Click(object sender, RoutedEventArgs e)
        {
            orderDetailDataGrid.Items.Remove(orderDetailDataGrid.SelectedItem);
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
