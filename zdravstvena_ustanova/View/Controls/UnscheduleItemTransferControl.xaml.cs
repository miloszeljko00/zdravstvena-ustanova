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
using System.Windows.Navigation;
using System.Windows.Shapes;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Controls
{
    public partial class UnscheduleItemTransferControl : UserControl, INotifyPropertyChanged
    {
        #region NotifyProperties
        private ItemTransferViewModel _itemTransferViewModel;
        public ItemTransferViewModel ItemTransferViewModel
        {
            get
            {
                return _itemTransferViewModel;
            }
            set
            {
                if (value != _itemTransferViewModel)
                {
                    _itemTransferViewModel = value;
                    OnPropertyChanged("ItemTransferViewModel");
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

        public ObservableCollection<ItemTransferViewModel> ItemTransferViewModels;

        public UnscheduleItemTransferControl(ItemTransferViewModel itemTransferViewModel, ObservableCollection<ItemTransferViewModel> itemTransferViewModels)
        {
            InitializeComponent();
            ItemTransferViewModels = itemTransferViewModels;
            ItemTransferViewModel = itemTransferViewModel;
            DataContext = ItemTransferViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var app = App.Current as App;

            app.ScheduledItemTransferController.Delete(ItemTransferViewModel.ScheduledItemTransfer.Id);
            ItemTransferViewModels.Remove(ItemTransferViewModel);

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
    }
}
