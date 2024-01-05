using BUS_MyShop;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTO_MyShop;
using System.Globalization;
using System.Threading;

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        BUS_Orders bus = BUS_Orders.Instance;
        BindingList<Order> orders;
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalPage = 0;
        private int _totalRecord = 0;
        public string beginDate;
        public string endDate;

        public Orders()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            deleteButton.IsEnabled = false;
            editButton.IsEnabled = false;
            try
            {
                beginDatePicker.SelectedDate = bus.GetAllOrders().Min(o => o.OrderDate);
                endDatePicker.SelectedDate = bus.GetAllOrders().Max(o => o.OrderDate);
            }
            catch (Exception ex)
            {
                beginDatePicker.SelectedDate = DateTime.Now;
                endDatePicker.SelectedDate = DateTime.Now;
            }
            LoadData();

            this.DataContext = this;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            EditOrderWindow editOrderWindow = new EditOrderWindow(new Order(), false);
            editOrderWindow.ShowDialog();

            if (editOrderWindow.DialogResult == true)
            {
                try
                {
                    BUS_MyShop.BUS_Orders.Instance.AddOrder(editOrderWindow.ReturnOrder.Id, editOrderWindow.ReturnOrder.CustomerId, editOrderWindow.ReturnOrder.OrderDate.Value);
                    foreach (OrderDetail orderDetail in editOrderWindow.orderDetailDataGrid.Items)
                    {
                        int quantity = orderDetail.Quantity!.Value;
                        BUS_MyShop.BUS_OrderDetails.Instance.AddOrderDetail(editOrderWindow.ReturnOrder.Id, orderDetail.ProductId, quantity);
                    }

                }
                catch (Exception ex)
                {
                    MessageWindow.Show(ex.Message, "Lỗi");
                    return;
                }
            }

            LoadData();

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var order = dataGrid_Orders.SelectedItem as Order;
            EditOrderWindow editOrderWindow = new EditOrderWindow(order, true);
            editOrderWindow.ShowDialog();
            if (editOrderWindow.DialogResult == true)
            {
                try
                {

                    //BUS_MyShop.BUS_OrderDetails.Instance.DeleteOrderDetailsByOrderId(editOrderWindow.ReturnOrder.Id);
                    foreach(OrderDetail orderDetail in editOrderWindow.orderDetailDataGrid.Items)
                    {
                        BUS_MyShop.BUS_OrderDetails.Instance.DeleteOrderDetail(editOrderWindow.ReturnOrder.Id, orderDetail.ProductId);
                    }

                    foreach (OrderDetail orderDetail in editOrderWindow.orderDetailDataGrid.Items)
                    {
                        int quantity = orderDetail.Quantity!.Value;
                        BUS_MyShop.BUS_OrderDetails.Instance.AddOrderDetail(editOrderWindow.ReturnOrder.Id, orderDetail.ProductId, quantity);
                    }

                }
                catch (Exception ex)
                {
                    MessageWindow.Show(ex.Message, "Lỗi");
                    return;
                }
            }
            LoadData();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            bus.DeleteOrder((dataGrid_Orders.SelectedItem as Order).Id);
            LoadData();

        }


        private void previousPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            currentPageTextBox.Text = _currentPage.ToString();
            LoadData();
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            currentPageTextBox.Text = _currentPage.ToString();
            LoadData();
        }

        private void logicTransmitPageSizeToLoadBooks()
        {
            try
            {
                int input = int.Parse(pageSizeTextBox.Text);
                if (1 <= input && input <= 100)
                {
                    LoadData();
                    return;
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }
            pageSizeTextBox.Text = _pageSize.ToString();
        }

        private void reloadButton_Click(object sender, RoutedEventArgs e)
        {
            logicTransmitPageSizeToLoadBooks();
        }

        private void changePageSize_KeyDowm(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logicTransmitPageSizeToLoadBooks();
            }
        }

        private void changeCurrentPage_KeyDowm(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    int input = int.Parse(currentPageTextBox.Text);
                    if (1 <= input && input <= _totalPage)
                    {
                        _currentPage = input;
                        LoadData();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // do nothing
                }
                currentPageTextBox.Text = _currentPage.ToString();
            }
        }

        private void LoadData()
        {


            int oldPageSize = _pageSize;
            int oldCurrentPage = _currentPage;
            try
            {
                _pageSize = int.Parse(pageSizeTextBox.Text);
                _currentPage = int.Parse(currentPageTextBox.Text);
            }
            catch (Exception ex)
            {
                _pageSize = oldPageSize;
                _currentPage = oldCurrentPage;
                pageSizeTextBox.Text = _pageSize.ToString();
                currentPageTextBox.Text = _currentPage.ToString();
            }


            if (oldPageSize != _pageSize)
            {
                _currentPage = 1;
                currentPageTextBox.Text = _currentPage.ToString();
            } else if (oldCurrentPage != _currentPage)
            {
                currentPageTextBox.Text = _currentPage.ToString();
            }

            int count;
            try
            {
                count = bus.GetCount(beginDatePicker.SelectedDate!.Value, endDatePicker.SelectedDate!.Value);
                orders = bus.GetOrders((_currentPage - 1) * _pageSize, _pageSize, BUS_Orders.SortType.OrderDate, false, beginDatePicker.SelectedDate!.Value, endDatePicker.SelectedDate!.Value);
            }
            catch(Exception ex)
            {
                count = 0;
                orders = new BindingList<Order>();
            }
            
            dataGrid_Orders.ItemsSource = orders;

            if (count != _totalRecord)
            {
                _totalRecord = count;
                _totalPage = _totalRecord / _pageSize + (_totalRecord % _pageSize == 0 ? 0 : 1);
                totalPageLabel.Content = _totalPage;
            }
            if (oldPageSize != _pageSize)
            {
                _totalPage = _totalRecord / _pageSize + (_totalRecord % _pageSize == 0 ? 0 : 1);
                totalPageLabel.Content = _totalPage;
            }

            previousPageButton.IsEnabled = _currentPage > 1;
            nextPageButton.IsEnabled = _currentPage < _totalPage;
        }

        private void beginDatePicker_changed(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            cultureInfo.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            beginDate = beginDatePicker.SelectedDate!.Value.ToShortDateString();
            beginDatePicker.Text = beginDate;
            if (endDatePicker.SelectedDate != null)
                LoadData();
        }

        private void endDatePicker_changed(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            cultureInfo.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            endDate = endDatePicker.SelectedDate!.Value.ToShortDateString();
            endDatePicker.Text = endDate;
            if (beginDatePicker.SelectedDate != null)
                LoadData();
        }

        private void dataGrid_Orders_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            deleteButton.IsEnabled = true;
            editButton.IsEnabled = true;
        }

        private void dataGrid_Orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderDetailWindow orderDetailWindow = new OrderDetailWindow(dataGrid_Orders.SelectedItem as Order);
            orderDetailWindow.ShowDialog();

        }
    }
}
