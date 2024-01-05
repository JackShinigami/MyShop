using GUI_MyShop;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels2 { get; set; }
        public Func<double, string> Formatter2 { get; set; }
        public ChartWindow()
        {
            InitializeComponent();
            var Data = BUS_OrderDetails.Instance.GetRevenueAndProfitByDay(new DateTime(2020, 1, 1), new DateTime(2030, 1, 1));
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<int> ((IEnumerable<int>)Data.Item2)
                },
                new LineSeries
                {
                    Title = "Lợi nhuận",
                    Values = new ChartValues<int> ((IEnumerable<int>)Data.Item3),
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
            var data2 = BUS_OrderDetails.Instance.GetSalesOfProducts(new DateTime(2020, 1, 1), new DateTime(2030, 1, 1));
            SeriesCollection2 = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Số lượng",
                    Values = new ChartValues<int> ((IEnumerable<int>) data2.Select(x => (int)x.Item2))
                }
            };

            Labels2 = new string[data2.Count];
            for (int i = 0; i < data2.Count; i++)
            {
                Labels2[i] = data2[i].Item1;
            }

            Formatter2 = value => value.ToString("0");

            DataContext = this;
        }
    }
}
