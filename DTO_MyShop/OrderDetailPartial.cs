using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_MyShop
{
    public partial class OrderDetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public OrderDetail Clone()
        {
            return (OrderDetail)this.MemberwiseClone();
        }
    }

}
