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
    /// Interaction logic for RequestMedicationApprovalPage.xaml
    /// </summary>
    public partial class RequestMedicationApprovalPage : Page, INotifyPropertyChanged
    {
        
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public string RequestMessage { get; set; }

        public Medication Medication { get; set; }

        #region NotifyProperties
        private string _medicationName;
        public string MedicationName
        {
            get
            {
                return _medicationName;
            }
            set
            {
                if (value != _medicationName)
                {
                    _medicationName = value;
                    OnPropertyChanged("MedicationName");
                }
            }
        }
        private int _medicationQuantity;
        public int MedicationQuantity
        {
            get
            {
                return _medicationQuantity;
            }
            set
            {
                if (value != _medicationQuantity)
                {
                    _medicationQuantity = value;
                    OnPropertyChanged("MedicationQuantity");
                }
            }
        }
        private MedicationType _medicationType;
        public MedicationType MedicationType
        {
            get
            {
                return _medicationType;
            }
            set
            {
                if (value != _medicationType)
                {
                    _medicationType = value;
                    OnPropertyChanged("MedicationType");
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
        public RequestMedicationApprovalPage(Medication medication)
        {
            InitializeComponent();
            DataContext = this;
            Medication = medication;
            MedicationName = medication.Name;
            MedicationType = medication.MedicationType;
            MedicationQuantity = medication.Quantity;

            Ingredients = new ObservableCollection<Ingredient>();
            foreach(var ingredient in medication.Ingredients)
            {
                Ingredients.Add(ingredient);
            }
            var app = Application.Current as App;
            Doctors = new ObservableCollection<Doctor>();
            foreach (var doctor in app.DoctorController.GetAll())
            {
                Doctors.Add(doctor);
            }
        }
        public RequestMedicationApprovalPage(Medication medication, Doctor selectedDoctor, string requestMessage)
        {
            InitializeComponent();
            DataContext = this;
            Medication = medication;
            MedicationName = medication.Name;
            MedicationType = medication.MedicationType;
            MedicationQuantity = medication.Quantity;

            Ingredients = new ObservableCollection<Ingredient>();
            foreach (var ingredient in medication.Ingredients)
            {
                Ingredients.Add(ingredient);
            }
            var app = Application.Current as App;
            Doctors = new ObservableCollection<Doctor>();
            foreach (var doctor in app.DoctorController.GetAll())
            {
                Doctors.Add(doctor);
                if (doctor.Id == selectedDoctor.Id)
                {
                    DoctorComboBox.SelectedItem = doctor;
                }
            }
            RequestMessage = requestMessage;
            RequestMessageTextBox.Text = requestMessage;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            RequestMessage = RequestMessageTextBox.Text;
            var selectedDoctor = (Doctor)DoctorComboBox.SelectedItem;
            MedicationApprovalRequest medicationApprovalRequest = new MedicationApprovalRequest(
                -1,
                Medication,
                selectedDoctor,
                RequestMessage,
                "",
               RequestStatus.WAITING_FOR_APPROVAL,
               false,
               false);
            var app = Application.Current as App;
            if (app.MedicationApprovalRequestController.CheckIfAlreadyWaitingForApproval(Medication))
            {
                MessageBox.Show("Zahtev za ovaj lek je već poslat!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            medicationApprovalRequest = app.MedicationApprovalRequestController.Create(medicationApprovalRequest);
            NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem == null)
            {
                OkButton.IsEnabled = false;
            }
            else
            {
                OkButton.IsEnabled = true;
            }
        }
    }
}
