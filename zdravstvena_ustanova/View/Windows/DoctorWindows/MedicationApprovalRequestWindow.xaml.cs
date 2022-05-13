﻿using System;
using System.Collections.Generic;
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
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for MedicationApprovalRequestWindow.xaml
    /// </summary>
    public partial class MedicationApprovalRequestWindow : Window
    {
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
        public MedicationApprovalRequest MedicationApprovalRequest { get; set; }
        public List<Ingredient> ListOfIngredientsForExactMedication { get; set; }
        #region NotifyProperties
        private RequestStatus _selectedRequestStatus;
        public RequestStatus SelectedRequestStatus
        {
            get
            {
                return _selectedRequestStatus;
            }
            set
            {
                if (value != _selectedRequestStatus)
                {
                    _selectedRequestStatus = value;
                    OnPropertyChanged("SelectedPatient");
                }
            }
        }
        #endregion
        public MedicationApprovalRequestWindow(MedicationApprovalRequest medicationApprovalRequest)
        {
            InitializeComponent();
            DataContext = this;
            MedicationApprovalRequest = medicationApprovalRequest;
            MedicationNameTextBox.Text = MedicationApprovalRequest.Medication.Name;
            RequestMessageTextBox.Text = medicationApprovalRequest.RequestMessage;
            ListOfIngredientsForExactMedication = new List<Ingredient>();
            foreach(Ingredient i in MedicationApprovalRequest.Medication.Ingredients)
            {
                ListOfIngredientsForExactMedication.Add(i);            
            }
            RequestStatusComboBox.ItemsSource = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {

        }
    }
}