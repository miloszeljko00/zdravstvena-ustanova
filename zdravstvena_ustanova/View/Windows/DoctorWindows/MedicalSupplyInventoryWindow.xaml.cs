using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for MedicalSupplyInventoryWindow.xaml
    /// </summary>
    public partial class MedicalSupplyInventoryWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<StoredItem> StoredItems { get; set; }
        #region NotifyProperties
        private Warehouse _warehouse;
        public Warehouse Warehouse
        {
            get
            {
                return _warehouse;
            }
            set
            {
                if (value != _warehouse)
                {
                    _warehouse = value;
                    OnPropertyChanged("Warehouse");
                }
            }
        }
        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        public MedicalSupplyInventoryWindow()
        {
            var app = Application.Current as App;

            Warehouse = app.WarehouseController.GetAll().SingleOrDefault();
            StoredItems = new ObservableCollection<StoredItem>(Warehouse.StoredItems);

            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click_NavigateBackMedicalSupplyInventory(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
