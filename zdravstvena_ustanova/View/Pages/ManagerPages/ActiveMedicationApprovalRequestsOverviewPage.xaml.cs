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

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for ActiveMedicationApprovalRequestsOverviewPage.xaml
    /// </summary>
    public partial class ActiveMedicationApprovalRequestsOverviewPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<MedicationApprovalRequest> ActiveMedicationApprovalRequests { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        #region NotifyProperties
        private string _requestMessage;
        public string RequestMessage
        {
            get
            {
                return _requestMessage;
            }
            set
            {
                if (value != _requestMessage)
                {
                    _requestMessage = value;
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
        public ActiveMedicationApprovalRequestsOverviewPage()
        {
            InitializeComponent();
            DataContext = this;
            ActiveMedicationApprovalRequests = new ObservableCollection<MedicationApprovalRequest>();
            Ingredients = new ObservableCollection<Ingredient>();
            var app = Application.Current as App;

            var requests = app.MedicationApprovalRequestController.GetByRequestStatus(RequestStatus.WAITING_FOR_APPROVAL);
            foreach(var request in requests)
            {
                ActiveMedicationApprovalRequests.Add(request);
            }
        }

        private void CancelRequestButton_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            var medicationApprovalRequest = (MedicationApprovalRequest)ActiveMedicationApprovalRequestsDataGrid.SelectedItem;

            if (medicationApprovalRequest == null) return;

            app.MedicationApprovalRequestController.Delete(medicationApprovalRequest.Id);
            ActiveMedicationApprovalRequests.Remove(medicationApprovalRequest);
        }

        private void ActiveMedicationApprovalRequestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ingredients.Clear();
            var medicationApprovalRequest = (MedicationApprovalRequest)ActiveMedicationApprovalRequestsDataGrid.SelectedItem;
            if (medicationApprovalRequest == null) return;
            foreach (var ingredient in medicationApprovalRequest.Medication.Ingredients) 
            {
                Ingredients.Add(ingredient);
            }
            RequestMessage = medicationApprovalRequest.RequestMessage;
        }
    }
}
