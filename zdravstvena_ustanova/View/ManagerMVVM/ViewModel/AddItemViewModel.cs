using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.ManagerMVVM.Model;
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View.ManagerMVVM.ViewModel
{
    public class AddItemViewModel : BindableBase
    {
        #region fields
        private ObservableCollection<ItemModel> _itemModels;
        public ObservableCollection<ItemModel> ItemModels
        {
            get => _itemModels;
            set => SetProperty(ref _itemModels, value);
        }

        private ObservableCollection<ItemType> _itemTypes;
        public ObservableCollection<ItemType> ItemTypes
        {
            get => _itemTypes;
            set => SetProperty(ref _itemTypes, value);
        }

        private string _itemName;
        public string ItemName
        {
            get => _itemName;
            set => SetProperty(ref _itemName, value);
        }

        private ItemType? _itemType;
        public ItemType? ItemType
        {
            get => _itemType;
            set => SetProperty(ref _itemType, value);
        }

        private string _itemDescription;
        public string ItemDescription
        {
            get => _itemDescription;
            set => SetProperty(ref _itemDescription, value);
        }
        #endregion

        #region constructors
        public AddItemViewModel(ObservableCollection<ItemModel> itemModels)
        {
            var app = Application.Current as App;
            ItemTypes = new ObservableCollection<ItemType>(app.ItemTypeController.GetAll());
            ItemModels = itemModels;

            OkCommand = new RelayCommand(Execute_OkCommand, CanExecute_OkCommand);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_CancelCommand);
        }
        #endregion

        #region commands

        public RelayCommand OkCommand { get; set; }
        private void Execute_OkCommand(object obj)
        {
            var app = Application.Current as App;

            var item = new Item(ItemName, ItemDescription, ItemType);

            item = app.ItemController.Create(item);

            ItemModels.Add(new ItemModel(item, 0));
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
        private bool CanExecute_OkCommand(object obj)
        {
            if (IsNullOrEmpty(ItemName)) return false;
            if (IsNullOrEmpty(ItemDescription)) return false;
            if(ItemType is null) return false;
            if (IsNullOrEmpty(ItemType.Name)) return false;

            return true;
        }

        public RelayCommand CancelCommand { get; set; }
        private void Execute_CancelCommand(object obj)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
        private bool CanExecute_CancelCommand(object obj)
        { 
            return true;
        }

        private bool IsNullOrEmpty(string obj)
        {
            return obj is null or "" || obj.Length == 0;
        }

        #endregion
    }
}
