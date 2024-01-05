using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows;
using DTO_MyShop;
namespace DAL_MyShop
{
    public class DAL_ImportData
    {
        private static DAL_ImportData? instance;
        private BookshopContext context = new BookshopContext();

        public static DAL_ImportData? Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAL_ImportData();
                return instance;
            }
        }

        public void ImportDataFromExcelFile(string filePath)
        {
            if (filePath != "")
            {
                List<Category> Catlist = new List<Category>();
                List<Product> Prodlist = new List<Product>();
                try
                {
                    var document = SpreadsheetDocument.Open(filePath, false);
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
                            Cell publisherCell = cells.FirstOrDefault(c => c?.CellReference == $"E{row}")!;
                            string stringPublisher = publisherCell!.InnerText;
                            string publisher = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(stringPublisher))
                                    .InnerText;
                            //CostPrice cell
                            Cell costPriceCell = cells.FirstOrDefault(c => c?.CellReference == $"F{row}")!;
                            string costPrice = costPriceCell!.InnerText;

                            //SellingPrice cell
                            Cell sellingPriceCell = cells.FirstOrDefault(c => c?.CellReference == $"G{row}")!;
                            string sellingPrice = sellingPriceCell!.InnerText;
                            //CategoryID cell
                            Cell categoryCell = cells.FirstOrDefault(c => c?.CellReference == $"H{row}")!;
                            string stringCategory = categoryCell!.InnerText;
                            string categoryID = stringTable.SharedStringTable
                                    .ElementAt(int.Parse(stringCategory)).InnerText;

                            //Quantity cell
                            Cell quantityCell = cells.FirstOrDefault(c => c?.CellReference == $"I{row}")!;
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
                                ImagePath = ""
                            });

                        }
                        row++;
                    } while (idCell?.InnerText.Length > 0);

                    //add to database
                    foreach (var item in Catlist)
                    {
                        DAL_ListCategories.Instance.AddCategory(item);
                    }
                    foreach (var item in Prodlist)
                    {
                        DAL_ListProducts.Instance.AddProduct(item);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
