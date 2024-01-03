using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Vml.Office;
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
using System.Windows.Shapes;
using BUS_MyShop;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace GUI_MyShop {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window {
        private Point startPoint;
        private bool isDragging = false;
        Rect restoreSize = new Rect(0, 0, 0, 0);
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }

        private ValidationResult? usernameNotEmpty = null; 
        private ValidationResult? passwordNotEmpty = null; 

        public LoginWindow() {
            InitializeComponent();
            Username = "";
            Password = "";
            StateChanged += LoginWindow_StateChanged;
            try { 
            if(BUS_User.Instance!.GetUsername() == null)
                BUS_User.Instance!.SaveUser("admin", "admin");
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //CategoriesWindow categoriesWindow = new CategoriesWindow();
            //categoriesWindow.Show();
            //ChartWindow chartWindow = new ChartWindow();
            //chartWindow.Show();
            CustomersWindow customersWindow = new CustomersWindow();
            customersWindow.Show();
        }

        #region Title Bar Event Handler


        private void LoginWindow_StateChanged(object? sender, EventArgs e) {
            if (WindowState == WindowState.Normal) {

            } else if (WindowState == WindowState.Maximized) {

            } else if (WindowState == WindowState.Minimized) {

            }

            // fade in the window
            DoubleAnimation animation = new DoubleAnimation(1, (Duration)TimeSpan.FromSeconds(0.1));
            this.BeginAnimation(OpacityProperty, animation);


            Debug.WriteLine(WindowState);
        }


        private void closeButton_Click(object sender, RoutedEventArgs e) {
            // fade out the window
            DoubleAnimation animation = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.2));
            animation.Completed += (s, _) => this.Close();
            this.BeginAnimation(TopProperty, new DoubleAnimation(500, (Duration)TimeSpan.FromSeconds(0.2)));
            this.BeginAnimation(OpacityProperty, animation);
        }

        private void maximizeButton_Click(object sender, RoutedEventArgs e) {
            // maximize the window with animation 

            // fade out the window
            DoubleAnimation animation = new DoubleAnimation(0.0, (Duration)TimeSpan.FromSeconds(0.1));
            this.BeginAnimation(OpacityProperty, animation);

            Task.Delay(200).ContinueWith(_ => {
                Dispatcher.Invoke(() => {
                    if (WindowState == WindowState.Maximized) {
                        WindowState = WindowState.Normal;
                    } else {
                        WindowState = WindowState.Maximized;
                    }
                });
            });
        }



        private void minimizeButton_Click(object sender, RoutedEventArgs e) {
           
            Task.Delay(300).ContinueWith(_ => {
                Dispatcher.Invoke(() => {
                    WindowState = WindowState.Minimized;
                });
            });

        }

        private void titleBarBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                maximizeButton_Click(sender, e);
                return;
            }

            if (WindowState == WindowState.Maximized) {
                startPoint = e.GetPosition(this);
                isDragging = true;
            } else {
                DragMove();
            }
        }


        private void titleBarBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            isDragging = false;
        }
        private void titleBarBorder_MouseMove(object sender, MouseEventArgs e) {

            if (isDragging) {
                if (WindowState == WindowState.Maximized) {
                    WindowState = WindowState.Normal;
                    Left = startPoint.X - RestoreBounds.Width / 2;
                    Top = startPoint.Y - 10;
                }
            }
            if (e.LeftButton == MouseButtonState.Pressed) {
                DragMove();
            }
        }

        private void titleBarBorder_MouseLeave(object sender, MouseEventArgs e) {
            isDragging = false;
        }



        #endregion

        private void loginButton_Click(object sender, RoutedEventArgs e) {

            Username = usernameTextBox.Text;
            Password = passwordPasswordBox.Password;

            var validation = new EmptyInputRule();

            usernameNotEmpty = validation.Validate(usernameTextBox.Text, null);
            if(usernameNotEmpty == null || !usernameNotEmpty!.IsValid) {
                resultTextBlock.Text = "Vui lòng nhập tên đăng nhập";
                return;
            }

            passwordNotEmpty = validation.Validate(passwordPasswordBox.Password, null);
            if (passwordNotEmpty == null || !passwordNotEmpty!.IsValid) {
                resultTextBlock.Text = "Vui lòng nhập mật khẩu";
                return;
            }
            
            //BUS_MyShop.BUS_User.Instance!.SaveUser("admin", "admin");

            if (BUS_User.Instance!.CheckLogin(Username, Password)) {
                resultTextBlock.Text = "";
                if (rememberCheckBox.IsChecked == true)
                {
                    BUS_User.Instance!.SetRemember(true);
                } else
                {
                    BUS_User.Instance!.SetRemember(false);
                }
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            } else {
                resultTextBlock.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
            }


        }



        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DataContext = this;
            // load remember me
            if (BUS_User.Instance!.GetRemember())
            {
                rememberCheckBox.IsChecked = true;
                usernameTextBox.Text = BUS_User.Instance!.GetUsername();
                passwordPasswordBox.Password = BUS_User.Instance!.GetPassword();
            } else
            {
                rememberCheckBox.IsChecked = false;
            }
        }

        private void passwordPasswordBox_LostFocus(object sender, RoutedEventArgs e) {
            // validate empty password
            var validation = new EmptyInputRule();
            passwordNotEmpty = validation.Validate(passwordPasswordBox.Password, null);

            var dynamicTemplateResource = FindResource("SpecialFocusPasswordBoxTemplate") as ControlTemplate;
            passwordPasswordBox.Template = dynamicTemplateResource;
            passwordPasswordBox.ApplyTemplate();

            var border = passwordPasswordBox!.Template!.FindName("ContainerBorder", passwordPasswordBox) as Border;
            var borderBrush = new SolidColorBrush();
            borderBrush.Color = ((SolidColorBrush)border!.BorderBrush).Color;
            border.BorderBrush = borderBrush;

            if (!passwordNotEmpty.IsValid) {

                var animation = new ColorAnimation();
                animation.From = new Color() { A = 0xff, R = 0x20, G = 0xa8, B = 0xbb };
                animation.To = new Color() { A = 0xff, R = 0xff, G = 0x00, B = 0x00 };
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                animation.AutoReverse = false;
                animation.RepeatBehavior = new RepeatBehavior(5);
                borderBrush.BeginAnimation(SolidColorBrush.ColorProperty, animation);

            } else {

                borderBrush.Color = new Color() { A = 0xff, R = 0x20, G = 0xa8, B = 0xbb };

            }
                
        }

        private void usernameTextBox_LostFocus(object sender, RoutedEventArgs e) {
            var validation = new EmptyInputRule();
            usernameNotEmpty = validation.Validate(usernameTextBox.Text, null);
            var dynamicTemplateResource = FindResource("SpecialFocusTextBoxTemplate") as ControlTemplate;
            usernameTextBox.Template = dynamicTemplateResource;
            usernameTextBox.ApplyTemplate();

            var border = usernameTextBox!.Template!.FindName("ContainerBorder", usernameTextBox) as Border;
            var borderBrush = new SolidColorBrush();
            borderBrush.Color = ((SolidColorBrush)border!.BorderBrush).Color;
            border.BorderBrush = borderBrush;

            if (!usernameNotEmpty.IsValid) {

                var animation = new ColorAnimation();
                animation.From = new Color() { A = 0xff, R = 0x20, G = 0xa8, B = 0xbb };
                animation.To = new Color() { A = 0xff, R = 0xff, G = 0x00, B = 0x00 };
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                animation.AutoReverse = false;
                animation.RepeatBehavior = new RepeatBehavior(5);
                borderBrush.BeginAnimation(SolidColorBrush.ColorProperty, animation);

            } else {

                borderBrush.Color = new Color() { A = 0xff, R = 0x20, G = 0xa8, B = 0xbb };

            }
        }


        private void inputBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // lost focus all input box
                passwordPasswordBox.Focus();
                usernameTextBox.Focus();
                // click login button
                loginButton_Click(sender, e);
            }
        }
    }
}
