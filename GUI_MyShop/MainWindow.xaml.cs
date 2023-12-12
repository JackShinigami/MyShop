using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BUS_MyShop;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Fluent;
using RibbonWindow = Fluent.RibbonWindow;
namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private bool isDragging = false;
        Rect restoreSize = new Rect(0, 0, 0, 0);
        public string Text { get; set; }
        public string MyProperty { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Text = "Hello World";
            MyProperty = "Hello World";

        }

        #region Title Bar Event Handler


        private void MainWindow_StateChanged(object? sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {

            } else if (WindowState == WindowState.Maximized)
            {

            } else if (WindowState == WindowState.Minimized)
            {

            }

            // fade in the window
            DoubleAnimation animation = new DoubleAnimation(1, (Duration)TimeSpan.FromSeconds(0.1));
            this.BeginAnimation(OpacityProperty, animation);


            Debug.WriteLine(WindowState);
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            // fade out the window
            DoubleAnimation animation = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.2));
            animation.Completed += (s, _) => this.Close();
            this.BeginAnimation(TopProperty, new DoubleAnimation(500, (Duration)TimeSpan.FromSeconds(0.2)));
            this.BeginAnimation(OpacityProperty, animation);
        }

        private void maximizeButton_Click(object sender, RoutedEventArgs e)
        {
            // maximize the window with animation 

            // fade out the window
            DoubleAnimation animation = new DoubleAnimation(0.0, (Duration)TimeSpan.FromSeconds(0.1));
            this.BeginAnimation(OpacityProperty, animation);

            Task.Delay(500).ContinueWith(_ => {
                Dispatcher.Invoke(() => {
                    if (WindowState == WindowState.Maximized)
                    {
                        WindowState = WindowState.Normal;
                    } else
                    {
                        WindowState = WindowState.Maximized;
                    }
                });
            });
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {

            Task.Delay(300).ContinueWith(_ => {
                Dispatcher.Invoke(() => {
                    WindowState = WindowState.Minimized;
                });
            });

        }

        private void titleBarBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                maximizeButton_Click(sender, e);
                return;
            }

            if (WindowState == WindowState.Maximized)
            {
                startPoint = e.GetPosition(this);
                isDragging = true;
            } else
            {
                DragMove();
            }
        }


        private void titleBarBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void titleBarBorder_MouseMove(object sender, MouseEventArgs e)
        {

            if (isDragging)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    Left = startPoint.X - RestoreBounds.Width / 2;
                    Top = startPoint.Y - 10;
                }
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void titleBarBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
