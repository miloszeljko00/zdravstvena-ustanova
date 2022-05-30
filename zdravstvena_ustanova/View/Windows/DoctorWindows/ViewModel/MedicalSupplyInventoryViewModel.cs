using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Windows.DoctorWindows.View;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows.ViewModel
{
    public class MedicalSupplyInventoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<StoredItem> StoredItems { get; set; }
        public MedicalSupplyInventoryWindow MedicalSupplyInventoryWindow { get; set; }
        #region NotifyProperties
        private Warehouse _warehouse;
        public RelayCommand CloseCommand { get; set; }
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
        public MedicalSupplyInventoryViewModel(MedicalSupplyInventoryWindow medicalSupplyInventoryWindow)
        {
            var app = Application.Current as App;
            MedicalSupplyInventoryWindow = medicalSupplyInventoryWindow;
            Warehouse = app.WarehouseController.GetAll().SingleOrDefault();
            StoredItems = new ObservableCollection<StoredItem>(Warehouse.StoredItems);
            CloseCommand = new RelayCommand(param => ExecuteClose());
        }
        private void ExecuteClose()
        {
            MedicalSupplyInventoryWindow.Close();
        }
    }
}
