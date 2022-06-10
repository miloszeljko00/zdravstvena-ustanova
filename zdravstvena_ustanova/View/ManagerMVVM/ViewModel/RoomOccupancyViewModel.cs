using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.ManagerMVVM.Model;
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View.ManagerMVVM.ViewModel
{
    public class RoomOccupancyViewModel : BindableBase
    {
        #region fields
        public RoomsModel RoomsModel { get; set; }
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        public NavigationService NavigationService { get; set; }
        #endregion

        #region constructors
        public RoomOccupancyViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;

            var app = Application.Current as App;
            RoomsModel = new RoomsModel(app.RoomController.GetAll());

            GenerateReportCommand = new RelayCommand(Execute_GenerateReportCommand, CanExecute_GenerateReportCommand);
        }
        #endregion

        #region commands
        public RelayCommand GenerateReportCommand { get; set; }
        private void Execute_GenerateReportCommand(object obj)
        {
            NavigationService.Navigate(new RoomGeneratedReportPage(RoomsModel.SelectedRoom, (DateTime)StartDate, (DateTime)EndDate));
        }
        private bool CanExecute_GenerateReportCommand(object obj)
        {
            return StartDate.HasValue && EndDate.HasValue && RoomsModel.SelectedRoom is not null;
        }
        #endregion
    }
}
