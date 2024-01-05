using BUS_MyShop;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public string Username { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels2 { get; set; }
        public Func<double, string> Formatter2 { get; set; }

        public List<string> Type = new List<string> { "Theo ngày","Theo tháng", "Theo năm" };
        public DateTime beginDate;
        public DateTime endDate;

        public Dashboard()
        {
            InitializeComponent();
            Username = "klE";
            this.DataContext = this;
            beginDate = (DateTime)BUS_Orders.Instance.GetAllOrders().Min(o => o.OrderDate);
            endDate = (DateTime)BUS_Orders.Instance.GetAllOrders().Max(o => o.OrderDate);
            
            cbx_chooseType.ItemsSource = Type;
            cbx_chooseType.SelectedIndex = 0;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var Data = BUS_OrderDetails.Instance.GetRevenueAndProfitByDay(beginDate, endDate);
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<int> (Data.Item2)
                },
                new LineSeries
                {
                    Title = "Lợi nhuận",
                    Values = new ChartValues<int> (Data.Item3),
                    PointGeometry = null
                },
            };

            Labels = new string[Data.Item1.Count];
            for (int i = 0; i < Data.Item1.Count; i++)
            {
                Labels[i] = Data.Item1[i].ToString("dd/MM/yyyy");
            }
            Formatter = value => value.ToString("C", new CultureInfo("vi-VN"));


            //Biểu đồ số lượng sản phẩm
            var data2 = BUS_OrderDetails.Instance.GetSalesOfProducts(beginDate, endDate);
            SeriesCollection2 = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Số lượng",
                    Values = new ChartValues<int> ( data2.Select(x => (int)x.Item2))
                }
            };

            Labels2 = new string[data2.Count];
            for (int i = 0; i < data2.Count; i++)
            {
                Labels2[i] = data2[i].Item1;
            }

            Formatter2 = value => value.ToString("0");
            salesChart.Series = SeriesCollection;
            salesChartFormatter.LabelFormatter = Formatter;
            salesChartLabels.Labels = Labels;

            productChart.Series = SeriesCollection2;
            productChartFormatter.LabelFormatter = Formatter2;
            productChartLabel.Labels = Labels2;
        }

        private void beginDatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            var popup = datePicker?.Template.FindName("PART_Popup", datePicker) as Popup;
            var calendar = popup?.Child as System.Windows.Controls.Calendar;
            if (cbx_chooseType.SelectedIndex == 0)
            {
               
            }
            else if (cbx_chooseType.SelectedIndex == 1)
            {
                if (calendar != null)
                {
                    calendar.DisplayMode = CalendarMode.Year;

                    calendar.DisplayModeChanged += (s, ev) =>
                    {
                        if (calendar.DisplayMode == CalendarMode.Month)
                        {
                            // chọn đại ngày 1 của tháng (tại khom biết tắt hộp thoại khi mới đổi tháng)
                            calendar.SelectedDate = new DateTime(calendar.DisplayDate.Year, calendar.DisplayDate.Month, 1);
                            beginDatePicker.SelectedDate = calendar.SelectedDate;
                            try
                            {
                                beginDatePicker.IsDropDownOpen = false;
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            beginDatePicker.IsDropDownOpen = true;
                        }
                    };
                }
                
            }
            else
            {
               
                
                if (calendar != null)
                {
                    calendar.DisplayMode = CalendarMode.Decade;

                    calendar.DisplayModeChanged += (s, ev) =>
                    {
                        if (calendar.DisplayMode == CalendarMode.Year)
                        {
                            // chọn đại ngày 1 của tháng (tại khom biết tắt hộp thoại khi mới đổi tháng)
                            calendar.SelectedDate = new DateTime(calendar.DisplayDate.Year, 1, 1);
                            beginDatePicker.SelectedDate = calendar.SelectedDate;
                            try
                            {
                                beginDatePicker.IsDropDownOpen = false;
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            beginDatePicker.IsDropDownOpen = true;
                        }
                    };
                }
            } 
                
        }

        private void endDatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            var popup = datePicker?.Template.FindName("PART_Popup", datePicker) as Popup;
            var calendar = popup?.Child as System.Windows.Controls.Calendar;
            if (cbx_chooseType.SelectedIndex == 0)
            {

            }
            else if (cbx_chooseType.SelectedIndex == 1)
            {
                if (calendar != null)
                {
                    calendar.DisplayMode = CalendarMode.Year;

                    calendar.DisplayModeChanged += (s, ev) =>
                    {
                        if (calendar.DisplayMode == CalendarMode.Month)
                        {
                            // chọn đại ngày 1 của tháng (tại khom biết tắt hộp thoại khi mới đổi tháng)
                            calendar.SelectedDate = new DateTime(calendar.DisplayDate.Year, calendar.DisplayDate.Month, DateTime.DaysInMonth(calendar.DisplayDate.Year, calendar.DisplayDate.Month));
                            endDatePicker.SelectedDate = calendar.SelectedDate;
                            try
                            {
                                endDatePicker.IsDropDownOpen = false;
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            endDatePicker.IsDropDownOpen = true;
                        }
                    };
                }
            }
            else
            {
                if (calendar != null)
                {
                    calendar.DisplayMode = CalendarMode.Decade;

                    calendar.DisplayModeChanged += (s, ev) =>
                    {
                        if (calendar.DisplayMode == CalendarMode.Year)
                        {
                            // chọn đại ngày 1 của tháng (tại khom biết tắt hộp thoại khi mới đổi tháng)
                            calendar.SelectedDate = new DateTime(calendar.DisplayDate.Year, 12, 31);
                            endDatePicker.SelectedDate = calendar.SelectedDate;
                            try
                            {
                                endDatePicker.IsDropDownOpen = false;
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            endDatePicker.IsDropDownOpen = true;
                        }
                    };
                }
            }
        }

        private void beginDatePicker_changed(object sender, SelectionChangedEventArgs e)
        {
            beginDate = (DateTime)beginDatePicker.SelectedDate;
            if(endDate != null)
                LoadData();
        }

        private void endDatePicker_changed(object sender, SelectionChangedEventArgs e)
        {
            endDate = (DateTime)endDatePicker.SelectedDate;
            if(beginDate != null)
                LoadData();
        }

        private void cbx_chooseType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbx_chooseType.SelectedIndex == 0)
            {
                CultureInfo cultureInfo = new CultureInfo("vi-VN");
                cultureInfo.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                cultureInfo.DateTimeFormat.DateSeparator = "/";
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            else if(cbx_chooseType.SelectedIndex == 1)
            {
                CultureInfo cultureInfo = new CultureInfo("vi-VN");
                cultureInfo.DateTimeFormat.ShortDatePattern = "MM/yyyy";
                cultureInfo.DateTimeFormat.DateSeparator = "/";
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            else
            {
                CultureInfo cultureInfo = new CultureInfo("vi-VN");
                cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy";
                cultureInfo.DateTimeFormat.DateSeparator = "/";
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
        }
    }
}
