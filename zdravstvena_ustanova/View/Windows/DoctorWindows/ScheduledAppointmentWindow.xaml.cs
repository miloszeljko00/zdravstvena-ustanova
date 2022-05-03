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
using System.Windows.Shapes;
using Model;
using Model.Enums;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for ScheduledAppointmentWindow.xaml
    /// </summary>
    public partial class ScheduledAppointmentWindow : Window, INotifyPropertyChanged
    {
        #region NotifyProperties
        private string _patientName;
        public string PatientName
        {
            get
            {
                return _patientName;
            }
            set
            {
                if (value != _patientName)
                {
                    _patientName = value;
                    OnPropertyChanged("PatientName");
                }
            }
        }

        private string _patientSurname;
        public string PatientSurname
        {
            get
            {
                return _patientSurname;
            }
            set
            {
                if (value != _patientSurname)
                {
                    _patientSurname = value;
                    OnPropertyChanged("PatientSurname");
                }
            }
        }

            private string _patientBirthday;
        public string PatientBirthday
        {
            get
            {
                return _patientBirthday;
            }
            set
            {
                if (value != _patientBirthday)
                {
                    _patientBirthday = value;
                    OnPropertyChanged("PatientBirthday");
                }
            }
        }


        private Anamnesis _anamnesis;
        public Anamnesis Anamnesis
        {
            get
            {
                return _anamnesis;
            }
            set
            {
                if (value != _anamnesis)
                {
                    _anamnesis = value;
                    OnPropertyChanged("Anamnesis");
                }
            }
        }


        public ObservableCollection<PrescribedMedicine> PrescribedMedicine { get; set; }
        #endregion
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public DoctorHomePageWindow DoctorHomePageWindow { get; set; }
        public MedicalExamination MedicalExamination { get; set; }


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
       
        public ScheduledAppointmentWindow(ScheduledAppointment selectedAppointment, DoctorHomePageWindow dhpw)
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            PatientName = selectedAppointment.Patient.Name;
            PatientSurname = selectedAppointment.Patient.Surname;
            PatientBirthday = selectedAppointment.Patient.DateOfBirth.ToString();
            doctorsName.Content = selectedAppointment.Doctor.Name;
            doctorsSurname.Content = selectedAppointment.Doctor.Surname;
            ScheduledAppointment = selectedAppointment;
            bloodTypeComboBox.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            DoctorHomePageWindow = dhpw;
            var me1 = app.MedicalExaminationController.FindByScheduledAppointmentId(selectedAppointment.Id);
            if(me1==null)
            {
                Anamnesis = new Anamnesis(-1);
                MedicalExamination = new MedicalExamination();
                PrescribedMedicine = new ObservableCollection<PrescribedMedicine>();
                MedicalExamination.ScheduledAppointment = selectedAppointment;

            }
            else
            {
                MedicalExamination = me1;
                if (me1.Anamnesis == null)
                {
                    Anamnesis = new Anamnesis(-1);
                } else
                {
                    Anamnesis = me1.Anamnesis;
                }

                if (me1.PrescribedMedicine == null)
                {
                    PrescribedMedicine = new ObservableCollection<PrescribedMedicine>();
                } else
                {
                    PrescribedMedicine = new ObservableCollection<PrescribedMedicine>();
                    foreach (PrescribedMedicine pm in me1.PrescribedMedicine)
                    {
                        PrescribedMedicine.Add(pm);
                    }
                }
            }
           
                   
        }

        private void Button_Click_Submit_TabAnamnesis(object sender, RoutedEventArgs e)
        {
            if (AnamnesisDiagnosisTextBoxInput.Text != "")
            {
                Anamnesis.Diagnosis = new string(AnamnesisDiagnosisTextBoxInput.Text);
            }
            if(AnamnesisConclusionTextBoxInput.Text != "")
            {
                Anamnesis.Conclusion = new string(AnamnesisConclusionTextBoxInput.Text);
            }
        }

        private void Button_Click_Cancel_TabAnamnesis(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da ponistite izmene?", "Ponistavanje Anamneze",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(answer==MessageBoxResult.Yes)
            {
                var app = Application.Current as App;
                if (app.AnamnesisController.GetById(Anamnesis.Id) != null)
                {
                    Anamnesis = app.AnamnesisController.GetById(Anamnesis.Id);
                }
                
            }

            // TODO Sve iz medical examination
        }

        private void Button_Click_FinalSubmit(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            
            List<PrescribedMedicine> prescribedMedicineBeforeRemoving = new List<PrescribedMedicine>();
            foreach(PrescribedMedicine pm in MedicalExamination.PrescribedMedicine)
            {
                prescribedMedicineBeforeRemoving.Add(pm);
            }
            MedicalExamination.PrescribedMedicine.Clear();
            /////////////////////////////////////////////////////
            foreach (PrescribedMedicine pm in PrescribedMedicine)
            {
                if(pm.Id==-1)
                {
                    PrescribedMedicine preMed = app.PrescribedMedicineController.Create(pm);
                    MedicalExamination.PrescribedMedicine.Add(preMed);
                }
                else
                {
                    app.PrescribedMedicineController.Update(pm);
                    MedicalExamination.PrescribedMedicine.Add(pm);
                }
                
            }
            /////////////////////////////////////////////////////
            if (Anamnesis.Id == -1)
            {
                Anamnesis = app.AnamnesisController.Create(Anamnesis);
                MedicalExamination.Anamnesis = Anamnesis;
            }
            else
            {
                app.AnamnesisController.Update(Anamnesis);
                MedicalExamination.Anamnesis=Anamnesis;
            }
            /////////////////////////////////////////////////////
            if (MedicalExamination.Id==-1)
            {
                
                MedicalExamination = app.MedicalExaminationController.Create(MedicalExamination);

            }
            else
            {
                var brojac = 0;
                var count = PrescribedMedicine.Count();
                foreach(PrescribedMedicine pm1 in prescribedMedicineBeforeRemoving)
                {
                    brojac = 0;
                    foreach(PrescribedMedicine pmFromProperty in PrescribedMedicine)
                    {
                        if(pmFromProperty.Id == pm1.Id)
                        {
                            break;
                        }
                        if(++brojac == count)
                        {
                            app.PrescribedMedicineController.Delete(pm1.Id);

                        }
                    }
                }
                app.MedicalExaminationController.Update(MedicalExamination);
            }
            /////////////////////////////////////////////////////
            if (AnamnesisDiagnosisTextBoxInput.Text != "")
            {
                Anamnesis.Diagnosis = new string(AnamnesisDiagnosisTextBoxInput.Text);
            }
            else if (AnamnesisConclusionTextBoxInput.Text != "")
            {
                Anamnesis.Conclusion = new string(AnamnesisConclusionTextBoxInput.Text);
            }
            else
            {
                app.AnamnesisController.Delete(Anamnesis.Id);
                MedicalExamination.Anamnesis = new Anamnesis(-1);
                app.MedicalExaminationController.Update(MedicalExamination);
            }
            ///////////////////////////////////////////////////////////
            Close();
        }

        private void Button_Click_Cancel_Appointment(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da otkazete pregled?", "Otkazivanje Pregleda", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            { 
                var app = Application.Current as App;
                app.ScheduledAppointmentController.Delete(ScheduledAppointment.Id);

                DoctorHomePageWindow.UpdateCalendar();
                this.Close();
            }
        }
      

        private void Button_Click_Update_Appointment(object sender, RoutedEventArgs e)
        {
            UpdateAppointment ua = new UpdateAppointment(ScheduledAppointment, DoctorHomePageWindow);
            ua.ShowDialog();
        }
        private void Button_Click_Add_Therapy(object sender, RoutedEventArgs e)
        {
            AddMedicineToTherapy addMedicineToTherapy = new AddMedicineToTherapy(PrescribedMedicine);
            addMedicineToTherapy.ShowDialog();
        }
        private void Button_Click_Edit_Therapy(object sender, RoutedEventArgs e)
        {
            if (dataGridTherapy.SelectedItem == null)
            {
                MessageBox.Show("Niste selektovali lek");
                return;
            }
            PrescribedMedicine pm = (PrescribedMedicine)dataGridTherapy.SelectedItem;
            UpdateMedicineInTherapy updateMedicineInTherapy = new UpdateMedicineInTherapy(PrescribedMedicine, pm);
            updateMedicineInTherapy.ShowDialog();
          
            
        }

        private void Button_Click_Remove_Therapy(object sender, RoutedEventArgs e)
        {
            PrescribedMedicine pm = (PrescribedMedicine)dataGridTherapy.SelectedItem;
            PrescribedMedicine.Remove(pm);
        }
    }
}
