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

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {

        public string Message { get; set; }
        public string BoxTitle { get; set; }
        public string OkButtonText { get; set; }

        public MessageWindow(string message, string boxTitle, string okButtonText)
        {
            InitializeComponent();
            Message = message;
            BoxTitle = boxTitle;
            OkButtonText = okButtonText;
        }

        public static void Show(string message, string boxTitle = "Thông báo", string okButtonText = "OK")
        {
            MessageWindow messageWindow = new MessageWindow(message, boxTitle, okButtonText);
            messageWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
