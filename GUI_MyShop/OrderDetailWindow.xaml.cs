using DTO_MyShop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for OrderDetailWindow.xaml
    /// </summary>
    public partial class OrderDetailWindow : Window
    {
        public Order ReturnOrder = new Order();
        BindingList<OrderDetail> OrderDetails = new BindingList<OrderDetail>();
        BindingList<Customer> Customers = new BindingList<Customer>();
        BindingList<Product> Products = new BindingList<Product>();
        public bool EditMode = false;


        public OrderDetailWindow(Order order)
        {

            InitializeComponent();
            ReturnOrder = order.Clone() as Order;

            // Load Customer
            Customers = BUS_MyShop.BUS_Customers.Instance.GetAllCustomers();
            // Load Product
            Products = BUS_MyShop.BUS_Products.Instance.GetAllProducts();

            // Load Order
            OrderDetails = BUS_MyShop.BUS_OrderDetails.Instance.GetOrderDetailsByOrderId(order.Id);
            orderDetailDataGrid.ItemsSource = OrderDetails;


            ReturnOrder.Customer = Customers.FirstOrDefault(c => c.Id == ReturnOrder.CustomerId);

            foreach (OrderDetail orderDetail in OrderDetails)
            {
                orderDetail.Product = Products.FirstOrDefault(p => p.Id == orderDetail.ProductId);
            }

            Title = "Chi tiết đơn hàng";
            titleLabel.Content = "Chi tiết đơn hàng";
            UpdateTotal();
            this.DataContext = ReturnOrder;
        }


        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void UpdateTotal()
        {
            var res = BUS_MyShop.BUS_OrderDetails.Instance.TotalPriceAndDiscount(OrderDetails.ToList());
            BUS_MyShop.NumberToVndConverter numberToVndConverter = new BUS_MyShop.NumberToVndConverter();
            totalPriceLabel.Content = numberToVndConverter.Convert(res.Item1, null, string.Empty, CultureInfo.CurrentCulture);
            discountLabel.Content = res.Item2.ToString("P2");
        }



        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
