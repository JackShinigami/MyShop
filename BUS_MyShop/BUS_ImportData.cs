using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO_MyShop;
using DAL_MyShop;
namespace BUS_MyShop
{
    public class BUS_ImportData
    {
        private static BUS_ImportData? instance;
        private DAL_ImportData dal = DAL_ImportData.Instance!;
        private BUS_ImportData()
        {

        }
        public static BUS_ImportData? Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_ImportData();
                return instance;
            }
        }

        public void ImportDataFromExcelFile(string filePath)
        {
            dal.ImportDataFromExcelFile(filePath);
        }
    }
}
