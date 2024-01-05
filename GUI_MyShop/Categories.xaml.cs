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

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : UserControl
    {
        BUS_Categories bus = BUS_Categories.Instance;
        BindingList<Category> categories;
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalPage = 0;
        private int _totalRecord = 0;

        public Categories()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            deleteButton.IsEnabled = false;
            editButton.IsEnabled = false;

            this.DataContext = this;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

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
            }
            else if (oldCurrentPage != _currentPage)
            {
                currentPageTextBox.Text = _currentPage.ToString();
            }

            int count = bus.GetCount();
            categories = bus.GetCategories((_currentPage - 1) * _pageSize, _pageSize);
            dataGrid_Categories.ItemsSource = categories;

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
    }
}
