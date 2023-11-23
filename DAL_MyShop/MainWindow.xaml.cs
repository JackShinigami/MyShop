using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTO_MyShop;

namespace DAL_MyShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookshopContext context;
        public MainWindow()
        {
            InitializeComponent();
            context = new BookshopContext();
        }
        //Ví dụ cách dùng DAL để lấy dữ liệu từ database-------------------------------------------------------------
        private BindingList<object> itemList;
        DAL_ListProducts dao = DAL_ListProducts.Instance;
        DAL_ListOrders daoOrder = DAL_ListOrders.Instance;
        DAL_ListOrderDetails daoOrderDetail = DAL_ListOrderDetails.Instance;
        DAL_ListCustomers daoCustomer = DAL_ListCustomers.Instance;
        private void click_LoadProduct(object sender, RoutedEventArgs e)
        {
            //dataGrid.ItemsSource = dao.GetProducts(0, 10, DAL_ListProducts.SortType.SellingPrice, false ,"", 0, 500);

            //dataGrid.ItemsSource = daoOrderDetail.GetBestSellingProducts(3);
            //List<dynamic> list = daoOrderDetail.GetBestSellingProducts(3);
            ////MessageBox.Show(list[0].ProductId);
            ///
            //var a = daoOrderDetail.GetRevenueAndProfit(new DateTime(2023, 1, 1), new DateTime(2023, 12, 3));
            //MessageBox.Show(a.Revenue.ToString() + " " + a.Profit.ToString());

            dataGrid.ItemsSource = daoOrderDetail.GetSalesOfProduct(new DateTime(2023, 1, 1), new DateTime(2023, 12, 3));
        }

        private void click_Add(object sender, RoutedEventArgs e)
        {
            Product p = new Product()
            {
                Id = "P23",
                ProductName = "Book 23",
                Author = "Author 23",
                PublishYear = 2021,
                Publisher = "Publisher 23",
                CostPrice = 100,
                SellingPrice = 200,
                Quantity = 10,
                CategoryId = "Cat1",
                ImagePath = ""
            };

            dao.AddProduct(p);
        }

        private void click_Update(object sender, RoutedEventArgs e)
        {
            //Product p = new Product()
            //{
            //    Id = "P23",
            //    ProductName = "Hello",
            //    Author = "Author 23",
            //    PublishYear = 2021,
            //    Publisher = "Publisher 23",
            //    CostPrice = 100,
            //    SellingPrice = 200,
            //    Quantity = 10,
            //    CategoryId = "Cat1",
            //    ImagePath = ""
            //};

            //dao.UpdateProduct("P23", p);

            Customer customer = new Customer()
            {
                Id = "C1",
                CustomerName = "Customer 23",
                Address = "Address 23",
                TelephoneNumber = "123456789"
            };

            daoCustomer.UpdateCustomer("C1", customer);
        }

        private void click_Delete(object sender, RoutedEventArgs e)
        {
            dao.DeleteProduct("P23");
        }

        private void click_Order(object sender, RoutedEventArgs e)
        {
            
            dataGrid.ItemsSource = daoOrder.GetOrders(0, 10, DAL_ListOrders.SortType.OrderDate, false, new DateTime(2023, 1, 1), new DateTime(2023, 1, 3));
        }

        private void click_OrderDetail(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = daoOrderDetail.GetOrderDetails();
        }

        private void click_LoadCustomer(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = daoCustomer.GetCustomers(0, 10, DAL_ListCustomers.SortType.CustomerName, false, "");
        }

        //-------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------

        public List<Category> GetCategories()
        {
            List<Category> categories;
            categories = context.Categories.ToList();
            return categories;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers;
            customers = context.Customers.ToList();
            return customers;
        }

        public List<Order> GetOrders()
        {
            List<Order> orders;
            orders = context.Orders.ToList();
            return orders;
        }

        public List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderDetails;
            orderDetails = context.OrderDetails.ToList();
            return orderDetails;
        }

        public List<Order> GetOrders(DateTime beginTime, DateTime endTime)
        {
            List<Order> orders;
            orders = context.Orders.Where(o => o.OrderDate >= beginTime && o.OrderDate <= endTime).ToList();
            return orders;
        }
        
        public List<Product> GetLowQuantityProduct(int lowQuantity)
        {
            List<Product> products;
            products = context.Products.Where(p => p.Quantity < lowQuantity).ToList();
            return products;
        }

        private void click_LoadCategory(object sender, RoutedEventArgs e)
        {
            IEnumerable<object> categories = from c in context.Categories
                                             select c;
            itemList = new BindingList<object>(categories.ToList());
            dataGrid.ItemsSource = itemList;
        }

        private void click_LoadProductWithCategory(object sender, RoutedEventArgs e)
        {
            IEnumerable<object> products = from p in context.Products 
                                           join c in context.Categories on p.CategoryId equals c.Id
                                           select new { p.Id, p.ProductName, p.Author, p.PublishYear, p.Publisher, p.CostPrice, p.SellingPrice, p.Quantity, c.CategoryName };
            itemList = new BindingList<object>(products.ToList());
            dataGrid.ItemsSource = itemList;
        }

       

        

        private void click_OrderWithCustomer(object sender, RoutedEventArgs e)
        {
            IEnumerable<object> orders = from o in context.Orders
                                         join c in context.Customers on o.CustomerId equals c.Id
                                         select new { o.Id, o.OrderDate, o.CustomerId, c.CustomerName, c.Address, c.TelephoneNumber};
            itemList = new BindingList<object>(orders.ToList());
            dataGrid.ItemsSource = itemList;
        }

       

        private void click_LoadFull(object sender, RoutedEventArgs e)
        {
            IEnumerable<object> orderDetails = from od in context.OrderDetails
                                               join o in context.Orders on od.OrderId equals o.Id
                                               join c in context.Customers on o.CustomerId equals c.Id
                                               join p in context.Products on od.ProductId equals p.Id
                                               join ca in context.Categories on p.CategoryId equals ca.Id
                                               select new { od.OrderId, od.ProductId, od.Quantity, o.OrderDate, c.CustomerName, c.Address, c.TelephoneNumber, p.ProductName, p.Author, p.PublishYear, p.Publisher, p.CostPrice, p.SellingPrice, ca.CategoryName };
            itemList = new BindingList<object>(orderDetails.ToList());
            dataGrid.ItemsSource = itemList;
        }


        private string getDataFile()
        {
            //Open file dialog to select file
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                return dlg.FileName;
            }
            else
            {
                return "";
            }
        }
      
        private void click_ImportData(object sender, RoutedEventArgs e)
        {
            string filename = getDataFile();
            if (filename != "")
            {
                List<Category> Catlist = new List<Category>();
                List<Product> Prodlist = new List<Product>();
                try
                {
                    var document = SpreadsheetDocument.Open(filename, false);
                    var wbPart = document.WorkbookPart!;
                    var sheets = wbPart.Workbook.Descendants<Sheet>()!;
                    
                    
                    //Categories
                    var sheet = sheets.FirstOrDefault(s => s.Name == "Categories");
                    var wsPart = (WorksheetPart)(wbPart!.GetPartById(sheet.Id!));
                    var stringTable = wbPart
                        .GetPartsOfType<SharedStringTablePart>()
                        .FirstOrDefault()!;
                    var cells = wsPart.Worksheet.Descendants<Cell>();

                    int row = 2;
                    Cell idCell;

                    do
                    {
                        idCell = cells.FirstOrDefault(
                        c => c?.CellReference == $"A{row}"
                        )!;

                        if (idCell?.InnerText.Length > 0)
                        {
                            string id = idCell.InnerText;
                            string catId = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(id))
                                    .InnerText;
                            //Name cell
                            Cell nameCell = cells.FirstOrDefault(
                                c => c?.CellReference == $"B{row}"
                            )!;
                            string stringId = nameCell!.InnerText;
                            string name = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(stringId))
                                    .InnerText;
                            Catlist.Add(new Category { Id = catId, CategoryName = name });
                        }
                        row++;
                    } while (idCell?.InnerText.Length > 0);
                    

                    //Products
                    sheet = sheets.FirstOrDefault(s => s.Name == "Products");
                    wsPart = (WorksheetPart)(wbPart!.GetPartById(sheet.Id!));
                    stringTable = wbPart
                        .GetPartsOfType<SharedStringTablePart>()
                        .FirstOrDefault()!;
                    cells = wsPart.Worksheet.Descendants<Cell>();

                    row = 2;
                    do
                    {
                        idCell = cells.FirstOrDefault(c => c?.CellReference == $"A{row}")!;

                        if (idCell?.InnerText.Length > 0)
                        {
                            string id = idCell.InnerText;
                            string prodId = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(id))
                                    .InnerText;
                            //Name cell
                            Cell nameCell = cells.FirstOrDefault(c => c?.CellReference == $"B{row}")!;
                            string stringId = nameCell!.InnerText;
                            string name = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(stringId))
                                    .InnerText;
                            //Author cell
                            Cell authorCell = cells.FirstOrDefault(c => c?.CellReference == $"C{row}")!;
                            string stringAuthor = authorCell!.InnerText;
                            string author = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(stringAuthor))
                                    .InnerText;
                            //PublishYear cell
                            Cell publishYearCell = cells.FirstOrDefault(c => c?.CellReference == $"D{row}")!;
                            string publishYear = publishYearCell!.InnerText;
                            
                            //Publisher cell
                            Cell publisherCell = cells.FirstOrDefault( c => c?.CellReference == $"E{row}")!;
                            string stringPublisher = publisherCell!.InnerText;
                            string publisher = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(stringPublisher))
                                    .InnerText;
                            //CostPrice cell
                            Cell costPriceCell = cells.FirstOrDefault( c => c?.CellReference == $"F{row}")!;
                            string costPrice = costPriceCell!.InnerText;
                            
                            //SellingPrice cell
                            Cell sellingPriceCell = cells.FirstOrDefault(c => c?.CellReference == $"G{row}" )!;
                            string sellingPrice = sellingPriceCell!.InnerText;
                            //CategoryID cell
                            Cell categoryCell = cells.FirstOrDefault( c => c?.CellReference == $"H{row}")!;
                            string stringCategory = categoryCell!.InnerText;
                            string categoryID = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(stringCategory)).InnerText;

                            //Quantity cell
                            Cell quantityCell = cells.FirstOrDefault( c => c?.CellReference == $"I{row}")!;
                            string quantity = quantityCell!.InnerText;
                            
                            
                            Prodlist.Add(new Product
                            {
                                Id = prodId,
                                ProductName = name,
                                Author = author,
                                PublishYear = int.Parse(publishYear),
                                Publisher = publisher,
                                CostPrice = int.Parse(costPrice),
                                SellingPrice = int.Parse(sellingPrice),
                                Quantity = int.Parse(quantity),
                                CategoryId = categoryID,
                                ImagePath =""
                            });
                        
                        }
                        row++;
                    } while (idCell?.InnerText.Length > 0);
                    
                    //add to database
                    context.Categories.AddRange(Catlist);
                    context.Products.AddRange(Prodlist);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
    }
}
