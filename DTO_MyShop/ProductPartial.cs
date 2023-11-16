using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_MyShop
{
    public partial class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Product Clone()
        {
            return (Product)this.MemberwiseClone();
        }
    }
}
