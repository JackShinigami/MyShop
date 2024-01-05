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
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window
    {

        public Category ReturnCategory = new Category();

        public EditCategoryWindow(Category Category)
        {

            InitializeComponent();
            ReturnCategory = Category.Clone() as Category;

            this.DataContext = ReturnCategory;
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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
