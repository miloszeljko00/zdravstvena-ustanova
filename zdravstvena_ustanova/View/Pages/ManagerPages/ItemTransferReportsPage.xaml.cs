using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zdravstvena_ustanova.View.Controls;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for ItemTransferReportsPage.xaml
    /// </summary>
    public partial class ItemTransferReportsPage : Page
    {
        public ObservableCollection<ItemTransferViewModel> ItemTransferViewModels { get; set; }
        public ItemTransferReportsPage()
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;

            var scheduledItemTransfers = app.ScheduledItemTransferController.GetAll();
            ItemTransferViewModels = new ObservableCollection<ItemTransferViewModel>();
            foreach (var scheduledItemTransfer in scheduledItemTransfers)
            {
                ItemTransferViewModels.Add(new ItemTransferViewModel(scheduledItemTransfer));
            }
        }

        private void RescheduleNewDateButton_Click(object sender, RoutedEventArgs e)
        {
            ItemTransferViewModel itvm = (ItemTransferViewModel)ItemTransfersDataGrid.SelectedItem;
            if (itvm == null)
            {
                MessageBox.Show("Odaberi zakazano premeštanje!", "Ništa nije odabrano", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new RescheduleItemTransfer(itvm, ItemTransferViewModels);
            MainWindow.Modal.IsOpen = true;
        }

        private void UnscheduleNewDateButton_Click(object sender, RoutedEventArgs e)
        {
            ItemTransferViewModel itvm = (ItemTransferViewModel)ItemTransfersDataGrid.SelectedItem;
            if(itvm == null)
            {
                MessageBox.Show("Odaberi zakazano premeštanje!", "Ništa nije odabrano", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new UnscheduleItemTransferControl(itvm, ItemTransferViewModels);
            MainWindow.Modal.IsOpen = true;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(App.ProjectPath + "/Resources/img/search-name.png")
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;

                // Use the brush to paint the button's background.
                SearchTextBox.Background = textImageBrush;
            }
            else
            {

                SearchTextBox.Background = null;
            }
        }
    }
}
