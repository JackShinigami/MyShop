using GUI_MyShop.Utilities;
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
using BUS_MyShop;
using DTO_MyShop;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.IdentityModel.Tokens;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : UserControl
    {

        BindingList<Product> products = [];

        const int MaximumPrice = int.MaxValue;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalPage = 0;
        private int _totalRecord = 0;
        private int _allProductCount = 0;
        private int _minPrice = 0;
        private int _maxPrice = MaximumPrice;
        private bool _isAscending = true;

        public bool IsAscending { get => _isAscending; set => _isAscending = value; }

        BUS_Products bus = BUS_Products.Instance!;


        public Products()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooks();
            deleteButton.IsEnabled = false;
            editButton.IsEnabled = false;

            this.DataContext = this;
        }

        private void LoadBooks()
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

            BUS_Products.SortType sortType = BUS_Products.SortType.Id;
            switch (sortComboBox.SelectedIndex)
            {
                case 0:
                    sortType = BUS_Products.SortType.Id;
                    break;
                case 1:
                    sortType = BUS_Products.SortType.ProductName;
                    break;
                case 2:
                    sortType = BUS_Products.SortType.CostPrice;
                    break;
                case 3:
                    sortType = BUS_Products.SortType.SellingPrice;
                    break;
                case 4:
                    sortType = BUS_Products.SortType.Quantity;
                    break;
                default:
                    sortType = BUS_Products.SortType.Id;
                    break;
            }


            _allProductCount = bus.GetAllProducts().Count;
            _totalRecord = bus.GetProducts(0, _allProductCount, sortType, IsAscending, searchTextBox.Text, _minPrice, _maxPrice).Count;
            products = bus.GetProducts((_currentPage - 1) * _pageSize, _pageSize, sortType, IsAscending, searchTextBox.Text, _minPrice, _maxPrice);


            foreach (var product in products)
            {
                if (product.ImagePath.IsNullOrEmpty())
                {
                    product.ImagePath = @"Assets\BookCoverPlaceholder.png";
                } else
                {
                    // do nothing
                }
            }
            booksListView.ItemsSource = products;

            _totalPage = _totalRecord / _pageSize + (_totalRecord % _pageSize == 0 ? 0 : 1);
            totalPageLabel.Content = _totalPage;
            previousPageButton.IsEnabled = _currentPage > 1;
            nextPageButton.IsEnabled = _currentPage < _totalPage;

        }

        private void booksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteButton.IsEnabled = true;
            editButton.IsEnabled = true;

        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadBooks();
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }


        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadBooks();
        }

        private void logicTransmitPriceFilterToLoadBooks()
        {
            try
            {
                int input = int.Parse(minPriceTextBox.Text);
                if (0 <= input && input <= _maxPrice)
                {
                    _minPrice = input;
                } else throw new Exception();
            }
            catch (Exception ex)
            {
                minPriceTextBox.Text = "";
                _minPrice = 0;
            }

            try
            {
                int input = int.Parse(maxPriceTextBox.Text);
                if (_minPrice <= input && input <= MaximumPrice)
                {
                    _maxPrice = input;
                } else throw new Exception();
            }
            catch (Exception ex)
            {
                maxPriceTextBox.Text = "";
                _maxPrice = MaximumPrice;
            }
            LoadBooks();

        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            logicTransmitPriceFilterToLoadBooks();
        }
        private void minPriceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logicTransmitPriceFilterToLoadBooks();
            }

        }

        private void maxPriceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logicTransmitPriceFilterToLoadBooks();
            }
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

        private void isAscendingToggleButton_Click(object sender, RoutedEventArgs e)
        {
            IsAscending = isAscendingToggleButton.IsChecked!.Value;
            LoadBooks();

        }

        private void logicTransmitPageSizeToLoadBooks()
        {
            try
            {
                int input = int.Parse(pageSizeTextBox.Text);
                if (1 <= input && input <= 100)
                {
                    LoadBooks();
                    return;
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }
            pageSizeTextBox.Text = _pageSize.ToString();
        }

        private void pageSizeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logicTransmitPageSizeToLoadBooks();
            }
        }


        private void previousPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            currentPageTextBox.Text = _currentPage.ToString();
            LoadBooks();
        }

        private void nextPageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            currentPageTextBox.Text = _currentPage.ToString();
            LoadBooks();
        }

        private void changeCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    int input = int.Parse(currentPageTextBox.Text);
                    if (1 <= input && input <= _totalPage)
                    {
                        _currentPage = input;
                        LoadBooks();
                        return;
                    }
                }
                catch (Exception)
                {
                    // do nothing
                }
                currentPageTextBox.Text = _currentPage.ToString();
            }
        }



        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

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
                        LoadBooks();
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
    }
}
