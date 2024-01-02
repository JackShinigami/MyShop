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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_MyShop
{
    /// <summary>
    /// Interaction logic for ComandButton.xaml
    /// </summary>
    public partial class ComandButton : UserControl
    {

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                       "Text",
                       typeof(string),
                       typeof(ComandButton),
                       new PropertyMetadata(default(string))
            );

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
                        "Image",
                        typeof(ImageSource),
                        typeof(ComandButton),
                        new PropertyMetadata(default(ImageSource))
            );

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
                        "Command",
                        typeof(ICommand),
                        typeof(ComandButton),
                        new PropertyMetadata(default(ICommand))
            );

        // a property to set/get the imgBorder background of the button
        public static readonly DependencyProperty ImageBorderBackgroundProperty = DependencyProperty.Register(
                        "ImageBorderBackground",
                        typeof(Brush),
                        typeof(ComandButton),
                        new PropertyMetadata(default(Brush))
            );

        public ComandButton()
        {
            InitializeComponent();
        }

        public Brush ImageBorderBackground
        {
            get => (Brush)GetValue(ImageBorderBackgroundProperty);
            set => SetValue(ImageBorderBackgroundProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ImageSource Image
        {
            get => (ImageSource)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        private static void OnImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComandButton)d;
            control.img.Source = e.NewValue as ImageSource;
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComandButton)d;
            control.txt.Text = e.NewValue.ToString();
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComandButton)d;
            control.Command = e.NewValue as ICommand;
        }
    }
}
