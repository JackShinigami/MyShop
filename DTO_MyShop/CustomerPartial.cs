using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_MyShop
{
    public partial class Customer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Customer Clone()
        {
            return (Customer)this.MemberwiseClone();
        }
    }
}
