using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.View.Controls;
using zdravstvena_ustanova.View.ManagerMVVM.Model;
using zdravstvena_ustanova.View.ManagerMVVM.View;
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View.ManagerMVVM.ViewModel
{
    public class ManagerItemsViewModel
    {

        #region fields
        public ItemsModel ItemsModel { get; set; }
        public NavigationService NavigationService { get; set; }
        #endregion

        #region constructors
        public ManagerItemsViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;

            ItemsModel = new ItemsModel();

            var app = Application.Current as App;
            
            var items = app.ItemController.GetAll();

            foreach (var item in items)
            {
                var totalCount = app.StoredItemController.GetTotalItemCount(item);
                var itemModel = new ItemModel(item, totalCount);
                ItemsModel.Items.Add(itemModel);
            }
            
            SearchBoxCommand = new RelayCommand(Execute_SearchBoxCommand, CanExecute_SearchBoxCommand);
            DeleteItemCommand = new RelayCommand(Execute_DeleteItemCommand, CanExecute_DeleteItemCommand);
            EditItemCommand = new RelayCommand(Execute_EditItemCommand, CanExecute_EditItemCommand);
            AddItemCommand = new RelayCommand(Execute_AddItemCommand, CanExecute_AddItemCommand);
        }
        #endregion

        #region commands
        public RelayCommand SearchBoxCommand { get; set; }
        private void Execute_SearchBoxCommand(object obj)
        {
            TextBox searchBox = obj as TextBox;

            if (searchBox.Text == "")
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
                searchBox.Background = textImageBrush;
            }
            else
            {

                searchBox.Background = null;
            }
        }
        private bool CanExecute_SearchBoxCommand(object obj)
        {
            return true;
        }

        public RelayCommand DeleteItemCommand { get; set; }
        private void Execute_DeleteItemCommand(object obj)
        {
            MainWindow.Modal.Content = new DeleteItemControl(ItemsModel.Items, (DataGrid)obj);
            MainWindow.Modal.IsOpen = true;
        }
        private bool CanExecute_DeleteItemCommand(object obj)
        {
            return ItemsModel.SelectedItem is not null;
        }

        public RelayCommand EditItemCommand { get; set; }
        private void Execute_EditItemCommand(object obj)
        {
            MainWindow.Modal.Content = new EditItemControl(ItemsModel.Items, (DataGrid)obj);
            MainWindow.Modal.IsOpen = true;
        }
        private bool CanExecute_EditItemCommand(object obj)
        {
            return ItemsModel.SelectedItem is not null;
        }

        public RelayCommand AddItemCommand { get; set; }
        private void Execute_AddItemCommand(object obj)
        {
            MainWindow.Modal.Content = new AddItemView(ItemsModel.Items);
            MainWindow.Modal.IsOpen = true;
        }
        private bool CanExecute_AddItemCommand(object obj)
        {
            return true;
        }
        #endregion
    }
}
