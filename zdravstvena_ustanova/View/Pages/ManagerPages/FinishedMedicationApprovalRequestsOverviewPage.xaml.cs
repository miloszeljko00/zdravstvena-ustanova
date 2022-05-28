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
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using zdravstvena_ustanova.View.Controls;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for FinishedMedicationApprovalRequestsOverviewPage.xaml
    /// </summary>
    public partial class FinishedMedicationApprovalRequestsOverviewPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<MedicationApprovalRequest> FinishedMedicationApprovalRequests { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        #region NotifyProperties
        private string _responseMessage;
        public string ResponseMessage
        {
            get
            {
                return _responseMessage;
            }
            set
            {
                if (value != _responseMessage)
                {
                    _responseMessage = value;
                    OnPropertyChanged("RequestMessage");
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
        public FinishedMedicationApprovalRequestsOverviewPage()
        {
            InitializeComponent();
            DataContext = this;
            FinishedMedicationApprovalRequests = new ObservableCollection<MedicationApprovalRequest>();
            Ingredients = new ObservableCollection<Ingredient>();
            var app = Application.Current as App;
           
            var disapprovedRequests = app.MedicationApprovalRequestController.GetByRequestStatus(RequestStatus.DISAPPROVED);
            foreach (var request in disapprovedRequests)
            {
                FinishedMedicationApprovalRequests.Add(request);
            }

            var approvedRequests = app.MedicationApprovalRequestController.GetByRequestStatus(RequestStatus.APPROVED);
            foreach (var request in approvedRequests)
            {
                FinishedMedicationApprovalRequests.Add(request);
            }
           
        }

        private void FinishedMedicationApprovalRequestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ingredients.Clear();
            var medicationApprovalRequest = (MedicationApprovalRequest)FinishedMedicationApprovalRequestsDataGrid.SelectedItem;
            if (medicationApprovalRequest == null) return;
            foreach (var ingredient in medicationApprovalRequest.Medication.Ingredients)
            {
                Ingredients.Add(ingredient);
            }
            ResponseMessage = medicationApprovalRequest.ResponseMessage;
        }

        private void EditMedicationButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FinishedMedicationApprovalRequestsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi lek!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var approvalRequest = (MedicationApprovalRequest)FinishedMedicationApprovalRequestsDataGrid.SelectedItem;
            if(approvalRequest.RequestStatus == RequestStatus.APPROVED)return;

            var medication = approvalRequest.Medication;

            MainWindow.Modal.Content = new EditMedication(null, medication);
            MainWindow.Modal.IsOpen = true;
        }

        private void RequestApprovalAgainButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FinishedMedicationApprovalRequestsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi lek!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var approvalRequest = (MedicationApprovalRequest)FinishedMedicationApprovalRequestsDataGrid.SelectedItem;
            if (approvalRequest.RequestStatus == RequestStatus.APPROVED) return;

            var medication = approvalRequest.Medication;
            NavigationService.Navigate(new RequestMedicationApprovalPage(medication, approvalRequest.ApprovingDoctor, approvalRequest.RequestMessage));
        }
    }
}
