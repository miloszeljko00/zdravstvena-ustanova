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
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for ScheduledAppointmentWindow.xaml
    /// </summary>
    public partial class ScheduledAppointmentWindow : Window, INotifyPropertyChanged
    {
        //Drag&Drop
        Point startPoint = new Point();
        //\Drag&Drop
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
        public ScheduledAppointment SpecialistScheduledAppointment { get; set; }
        public ObservableCollection<LabAnalysisComponent> LabAnalysisComponents {get;set;}
        public ObservableCollection<LabAnalysisComponent> LabAnalysisComponents2 { get; set; }
        public LabAnalysisComponent LabAnalysisComponent { get; set; }
        public ObservableCollection<Specialty> Specialties { get; set; }
        public SolidColorBrush Brush { get; set; }
        public Doctor SelectedDoctor;

        private Specialty _selectedSpecialty;
        public Specialty SelectedSpecialty
        {
            get
            {
                return _selectedSpecialty;
            }
            set
            {
                if (value != _selectedSpecialty)
                {
                    _selectedSpecialty = value;
                    OnPropertyChanged("SelectedSpecialty");
                }
            }
        }
        private List<Doctor> _doctorsBySpecialty { get; set; }
        public List<Doctor> DoctorsBySpecialty
        {
            get
            {
                return _doctorsBySpecialty;
            }
            set
            {
                if (value != _doctorsBySpecialty)
                {
                    _doctorsBySpecialty = value;
                    OnPropertyChanged("DoctorsBySpecialty");
                }
            }
        }

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
            //NEBITNE STVARI
            LabAnalysisComponents2 = new ObservableCollection<LabAnalysisComponent>();
            //\NEBTINE STVARI
            var app = Application.Current as App;
            PatientName = selectedAppointment.Patient.Name;
            PatientSurname = selectedAppointment.Patient.Surname;
            PatientBirthday = selectedAppointment.Patient.DateOfBirth.ToString();
            doctorsName.Content = selectedAppointment.Doctor.Name;
            doctorsSurname.Content = selectedAppointment.Doctor.Surname;
            ScheduledAppointment = selectedAppointment;
            //SpecialistScheduledAppointment = new ScheduledAppointment(-1);
            var specialties = app.SpecialtyController.GetAll();
            Specialties = new ObservableCollection<Specialty>(specialties);
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
            //Drag&Drop
            LabAnalysisComponents = new ObservableCollection<LabAnalysisComponent>();
            var components = app.LabAnalysisComponentController.GetAll();
            foreach (LabAnalysisComponent lac in components)
            {
                LabAnalysisComponents.Add(lac);
            }
            //\Drag&Drop
            //\

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
                app.MedicalExaminationController.Delete(MedicalExamination.Id);

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
            AddMedicineToTherapy addMedicineToTherapy = new AddMedicineToTherapy(PrescribedMedicine, ScheduledAppointment);
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
            if (dataGridTherapy.SelectedItem == null)
            {
                MessageBox.Show("Niste selektovali lek");
                return;
            }
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da obirsete lek iz terapije?", "Brisanje leka iz terapije...", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                PrescribedMedicine pm = (PrescribedMedicine)dataGridTherapy.SelectedItem;
                PrescribedMedicine.Remove(pm);
            }
            return;
        }
        //Drag&Drop
        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(LabAnalysisComponent)) || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(LabAnalysisComponent)))
            {
                LabAnalysisComponent labAnalysisComponent = e.Data.GetData(typeof(LabAnalysisComponent)) as LabAnalysisComponent;
                //LabAnalysisComponents.Remove(labAnalysisComponent);
                LabAnalysisComponents2.Add(labAnalysisComponent);
            }
        }

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem == null) return;

                // Find the data behind the ListViewItem
                LabAnalysisComponent labAnalysisComponent = (LabAnalysisComponent)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject(typeof(LabAnalysisComponent), labAnalysisComponent);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        //\Drag&Drop

        private void Button_Click_Submit_Request_For_Specialist(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            Patient patient = ScheduledAppointment.Patient;
            string specialtiesComboBoxParameter = "";
            string doctorsBySpeciltyComboBoxParameter = "";
            string selectedTime = "";
            int startTime = 0;
            int endTime = 0;
            if (!(specialtiesComboBox.SelectedItem == null))
            {
                specialtiesComboBoxParameter = specialtiesComboBox.SelectedItem.ToString();
            }
            if (!(doctorsBySpecialtyComboBox.SelectedItem == null))
            {
                doctorsBySpeciltyComboBoxParameter = doctorsBySpecialtyComboBox.SelectedItem.ToString();
                SelectedDoctor = (Doctor)doctorsBySpecialtyComboBox.SelectedItem;
            }
            //if(specialtiesComboBox.SelectedItem == null || doctorsBySpecialtyComboBox.SelectedItem==null)
            //{
            //    MessageBox.Show("Morate odabrati specijalnost i lekara!");
            //    return;
            //}

            //if(TimeForSpecialistComboBox.SelectedItem == null)
            //{
            //    MessageBox.Show("Morate odabrati vreme");
            //        return;
            //}
            if (!(TimeForSpecialistComboBox.SelectedItem == null))
            {
                selectedTime = ((ComboBoxItem)TimeForSpecialistComboBox.SelectedItem).Content.ToString();
                startTime = int.Parse(selectedTime);
                endTime = int.Parse(selectedTime) + 1;
            }

            //if (requestForSpecialistDataPicker.SelectedDate == null)
            //{
            //    MessageBox.Show("Morate izabrati datum");
            //    return;
            //}
            //if(requestForSpecialistDataPicker.SelectedDate <= DateTime.Now)
            //{
            //    MessageBox.Show("Ne mozete zakazati termin u proslost!");
            //    return;

            //}
            DateTime? dateForValidation = requestForSpecialistDataPicker.SelectedDate;
            if (app.ScheduledAppointmentController.ValidateFormForSpecialistAppointment(specialtiesComboBoxParameter, doctorsBySpeciltyComboBoxParameter, selectedTime, dateForValidation))
            {


                DateTime selectedDate = (DateTime)requestForSpecialistDataPicker.SelectedDate;
                if(app.ScheduledAppointmentController.ValidateTime(selectedDate))
                {
                    DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, startTime, 0, 0);
                    DateTime endDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, endTime, 0, 0);
                    SpecialistScheduledAppointment = new ScheduledAppointment(startDate, endDate, AppointmentType.SPECIALIST_APPOINTMENT, ScheduledAppointment.Patient.Id,
                        SelectedDoctor.Id, SelectedDoctor.Room.Id);
                    SpecialistScheduledAppointment = app.ScheduledAppointmentController.Create(SpecialistScheduledAppointment);
                    DoctorHomePageWindow.UpdateCalendar();
                    this.Close();
                }
            }
            else
            {
                ///////////////////////////////// FENSI VALIDACIJA KRS KOD
                if (string.IsNullOrEmpty(specialtiesComboBox.Text))
                {
                    selectedSpecialtyPreventErrorTextBlock.Visibility = Visibility.Visible;
                    submitButton.IsEnabled = false;
                }
                else
                {
                    selectedSpecialtyPreventErrorTextBlock.Visibility = Visibility.Hidden;
                    CheckIfCanEnableSubmitButton();
                }
                if (string.IsNullOrEmpty(doctorsBySpecialtyComboBox.Text))
                {
                    selectedDoctorPreventErrorTextBlock.Visibility = Visibility.Visible;
                    submitButton.IsEnabled = false;
                }
                else
                {
                    selectedDoctorPreventErrorTextBlock.Visibility = Visibility.Hidden;
                    CheckIfCanEnableSubmitButton();
                }
                if (requestForSpecialistDataPicker.SelectedDate < DateTime.Now || requestForSpecialistDataPicker.SelectedDate == null)
                {
                    selectedDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                    submitButton.IsEnabled = false;
                }
                else
                {
                    selectedDatePreventErrorTextBlock.Visibility = Visibility.Hidden;
                    CheckIfCanEnableSubmitButton();
                }
                if (string.IsNullOrEmpty(TimeForSpecialistComboBox.Text))
                {
                    selectedTimePreventErrorTextBlock.Visibility = Visibility.Visible;
                    submitButton.IsEnabled = false;
                }
                else
                {
                    selectedTimePreventErrorTextBlock.Visibility = Visibility.Hidden;
                    CheckIfCanEnableSubmitButton();
                }
                ////////////////////////////////////////////////////////////////////////
            }
        }

        private void CheckIfCanEnableSubmitButton()
        {
            if (string.IsNullOrEmpty(specialtiesComboBox.Text) || string.IsNullOrEmpty(doctorsBySpecialtyComboBox.Text) || requestForSpecialistDataPicker.SelectedDate < DateTime.Now || requestForSpecialistDataPicker.SelectedDate == null || string.IsNullOrEmpty(TimeForSpecialistComboBox.Text))
            {
                submitButton.IsEnabled = false;
            }
            else
            {
                submitButton.IsEnabled = true;
            }
        }

        private void Button_Click_Cancel_Request_For_Specialist(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da ponistite izmene?", "Ponistavanje zahteva", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                specialtiesComboBox.SelectedItem = null;
                doctorsBySpecialtyComboBox.SelectedItem = null;
                requestForSpecialistDataPicker.SelectedDate = null;
                TimeForSpecialistComboBox.SelectedItem = null;
                return;
            }
            
        }

        private void specialtiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = Application.Current as App;
            var doctors = app.DoctorController.GetAll();
            SelectedSpecialty = (Specialty)specialtiesComboBox.SelectedItem;
            DoctorsBySpecialty = app.SpecialtyController.GetDoctorsBySpecialty(SelectedSpecialty, doctors);
        }

        private void specialtiesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(specialtiesComboBox.Text))
            {
                selectedSpecialtyPreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                selectedSpecialtyPreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
        }

        private void doctorsBySpecialtyComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(doctorsBySpecialtyComboBox.Text))
            {
                selectedDoctorPreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                selectedDoctorPreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
        }

        private void requestForSpecialistDataPicker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (requestForSpecialistDataPicker.SelectedDate < DateTime.Now || requestForSpecialistDataPicker.SelectedDate == null)
            {
                selectedDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                selectedDatePreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
        }

        private void TimeForSpecialistComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TimeForSpecialistComboBox.Text))
            {
                selectedTimePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                selectedTimePreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
        }
        private void jmbgTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(jmbgTextBox.Text))
            {
                jmbgTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                jmbgTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }
        }

        private void lboTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(lboTextBox.Text))
            {
                lboTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                lboTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }
        }

        private void roomTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(roomTextBox.Text))
            {
                roomTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                roomTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }
        }
        private void causeOfHospitalizationTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(causeOfHospitalizationTextBox.Text))
            {
                causeOfHospitalizationTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                causeOfHospitalizationTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }
        }

        private void backingDiseasesTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(backingDiseasesTextBox.Text))
            {
                backingDiseasesTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                backingDiseasesTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }
        }

        private void requestedDateOfAdmissionDatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (requestedDateOfAdmissionDatePicker.SelectedDate == null || requestedDateOfAdmissionDatePicker.SelectedDate < DateTime.Now)
            {
                requestedDateOfAdmissionDatePicker.BorderBrush = Brushes.Red;
                preventErrorRequestedDateForAdmissionTextBlock.Visibility = Visibility.Visible;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                requestedDateOfAdmissionDatePicker.BorderBrush = Brush;
                preventErrorRequestedDateForAdmissionTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableHospitalizeButton();
            }
        }

        private void CheckIfCanEnableHospitalizeButton()
        {
            if(requestedDateOfAdmissionDatePicker.SelectedDate == null || requestedDateOfAdmissionDatePicker.SelectedDate < DateTime.Now ||
                string.IsNullOrEmpty(backingDiseasesTextBox.Text) || string.IsNullOrEmpty(causeOfHospitalizationTextBox.Text) || string.IsNullOrEmpty(lboTextBox.Text) || string.IsNullOrEmpty(jmbgTextBox.Text))
            {
                hospitalizeButton.IsEnabled = false;
            }
            else
            {

                hospitalizeButton.IsEnabled = true;
            }
        }

        private void dateOfAdmissionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dateOfAdmissionTextBox.Text))
            {
                dateOfAdmissionTextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                dateOfAdmissionTextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }
        }

        private void releaseDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(releaseDateTextBox.Text))
            {
                releaseDateTextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                releaseDateTextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }
        }

        private void room2TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(room2TextBox.Text))
            {
                room2TextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                room2TextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }
        }

        private void hoursOfFanSupportTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(hoursOfFanSupportTextBox.Text))
            {
                hoursOfFanSupportTextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                hoursOfFanSupportTextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }
        }

        private void releaseKindComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(releaseKindComboBox.Text))
            {
                preventErrorReleseaseKindTextBlock.Visibility = Visibility.Visible;
                releaseButton.IsEnabled = false;
            }
            else
            {
                preventErrorReleseaseKindTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableReleaseButton();
            }
        }

        private void CheckIfCanEnableReleaseButton()
        {
            if (string.IsNullOrEmpty(dateOfAdmissionTextBox.Text) || string.IsNullOrEmpty(releaseDateTextBox.Text) || string.IsNullOrEmpty(room2TextBox.Text) ||
                string.IsNullOrEmpty(hoursOfFanSupportTextBox.Text) || string.IsNullOrEmpty(releaseKindComboBox.Text))
            {
                releaseButton.IsEnabled = false;
            }
            else
            {

                releaseButton.IsEnabled = true;
            }
        }

        private void hospitalizeButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrEmpty(jmbgTextBox.Text))
            {
                jmbgTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                jmbgTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }
            

            
           
            if (string.IsNullOrEmpty(lboTextBox.Text))
            {
                lboTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                lboTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }

            if (string.IsNullOrEmpty(roomTextBox.Text))
            {
                roomTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                roomTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }

            if (string.IsNullOrEmpty(causeOfHospitalizationTextBox.Text))
            {
                causeOfHospitalizationTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                causeOfHospitalizationTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }
            

            
            if (string.IsNullOrEmpty(backingDiseasesTextBox.Text))
            {
                backingDiseasesTextBox.BorderBrush = Brushes.Red;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                backingDiseasesTextBox.BorderBrush = Brush;
                CheckIfCanEnableHospitalizeButton();
            }



            if (requestedDateOfAdmissionDatePicker.SelectedDate == null || requestedDateOfAdmissionDatePicker.SelectedDate < DateTime.Now)
            {
                requestedDateOfAdmissionDatePicker.BorderBrush = Brushes.Red;
                preventErrorRequestedDateForAdmissionTextBlock.Visibility = Visibility.Visible;
                hospitalizeButton.IsEnabled = false;
            }
            else
            {
                requestedDateOfAdmissionDatePicker.BorderBrush = Brush;
                preventErrorRequestedDateForAdmissionTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableHospitalizeButton();
            }

        }

        private void releaseButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(dateOfAdmissionTextBox.Text))
            {
                dateOfAdmissionTextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                dateOfAdmissionTextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }



            if (string.IsNullOrEmpty(releaseDateTextBox.Text))
            {
                releaseDateTextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                releaseDateTextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }


            if (string.IsNullOrEmpty(room2TextBox.Text))
            {
                room2TextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                room2TextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }



            if (string.IsNullOrEmpty(hoursOfFanSupportTextBox.Text))
            {
                hoursOfFanSupportTextBox.BorderBrush = Brushes.Red;
                releaseButton.IsEnabled = false;
            }
            else
            {
                hoursOfFanSupportTextBox.BorderBrush = Brush;
                CheckIfCanEnableReleaseButton();
            }



            if (string.IsNullOrEmpty(releaseKindComboBox.Text))
            {
                preventErrorReleseaseKindTextBlock.Visibility = Visibility.Visible;
                releaseButton.IsEnabled = false;
            }
            else
            {
                preventErrorReleseaseKindTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableReleaseButton();
            }
        }
    }
}
