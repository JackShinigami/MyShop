using DTO_MyShop;
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
using BUS_MyShop;

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for ProductDetailWindow.xaml
    /// </summary>
    public partial class ProductDetailWindow : Window
    {

        public ProductDetailWindow(Product product)
        {
            InitializeComponent();
            this.DataContext = product;

            BUS_Categories bus = BUS_Categories.Instance;
            var categories = bus.GetCategories(0, int.MaxValue);
            string? cat = categories!.Where(c => c.Id == product.CategoryId)!.FirstOrDefault()!.CategoryName;
            categoryTextBlock.Text = cat;

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }


}
