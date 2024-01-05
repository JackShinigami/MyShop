using DTO_MyShop;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {

        public Product ReturnProduct = new Product();

        public AddProductWindow(Product product)
        {

            InitializeComponent();
            ReturnProduct = product.Clone() as Product;
            if (ReturnProduct.ImagePath.IsNullOrEmpty())
                ReturnProduct.ImagePath = @"Assets\BookCoverPlaceholder.png";

            this.DataContext = ReturnProduct;
        }

        private void ExitWindoWithAnimation()
        {
            // fade out and sweep to top animation
            DoubleAnimation animation = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.2));
            animation.Completed += (s, _) => this.Close();
            this.BeginAnimation(OpacityProperty, animation);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ExitWindoWithAnimation();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ExitWindoWithAnimation();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            ExitWindoWithAnimation();
        }

        private void changeCoverButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif, *.bmp, *.webp;) | *.jpg; *.jpeg; *.png; *.gif; *.bmp; *.webp;";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReturnProduct.ImagePath = openFileDialog.FileName;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}

