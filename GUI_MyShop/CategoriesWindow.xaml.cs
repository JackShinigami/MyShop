﻿using System;
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
using BUS_MyShop;
using DTO_MyShop;
using Microsoft.Data.SqlClient;

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        BUS_Categories bus = BUS_Categories.Instance;
        BindingList<Category> categories;
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalPage = 0;
        private int _totalRecord = 0;

        public CategoriesWindow()
        {
            InitializeComponent();
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
            _currentPage = int.Parse(currentPageTextBox.Text);


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
