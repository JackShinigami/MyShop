using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using GUI_MyShop.Utilities;

namespace GUI_MyShop
{
    class Navigation: GuiBase
    {   
        private Object _currentView;

        public Object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand DashboardCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand CategoriesCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand CustomersCommand { get; set; }

        public Navigation()
        {
            DashboardCommand = new RelayCommand(o => CurrentView = new Dashboard());
            ProductsCommand = new RelayCommand(o => CurrentView = new Products());
            CategoriesCommand = new RelayCommand(o => CurrentView = new Categories());
            OrdersCommand = new RelayCommand(o => CurrentView = new Orders());
            CustomersCommand = new RelayCommand(o => CurrentView = new Customers());

            CurrentView = new Dashboard();
        }
    }
}
