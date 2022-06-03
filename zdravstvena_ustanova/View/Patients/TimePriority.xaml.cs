using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace zdravstvena_ustanova.View
{
    public class DoctorsShift
    {
        public string Time { get; set; }
        public string Doctor { get; set; }
        public DoctorsShift()
        {
            Time = "";
            Doctor = "";
        }
    }
    public partial class TimePriority : Window
    {
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments;
        public ObservableCollection<DoctorsShift> Dates;
        public TimePriority()
        {
            InitializeComponent();
            Dates = new ObservableCollection<DoctorsShift>();
            string[] times = {" /", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };

            timeComboBox.ItemsSource = times;
            DateTime today = DateTime.Now;
            today = today.AddMinutes(-today.Minute);
            today = today.AddHours(1);
            if (today.Hour >= 20)
            {
                today = new DateTime(today.Year, today.Month, today.Day + 1, 7, 0, 0);
            }
            if (today.Hour <= 7)
            {
                today = new DateTime(today.Year, today.Month, today.Day, 7, 0, 0);
            }
            int days = DateTime.DaysInMonth(2022, DateTime.Now.Month);
            int to = today.Day + 4;
            if (to > days) { to -= days; }
            while (true)
            {
                if (today.Hour == 21) { today = today.AddDays(1); today = today.AddHours(-14); }
                if (today.Day == to) break;
                DoctorsShift ds = new DoctorsShift();
                ds.Time = today.ToString("dd.MM.yyyy. HH:mm");
                Dates.Add(ds);
                today = today.AddHours(1);
            }
            var app = Application.Current as App;
            ScheduledAppointments = new ObservableCollection<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            foreach (ScheduledAppointment sa in ScheduledAppointments)
            {
                foreach (DoctorsShift ds in Dates)
                {
                    if(ds.Time == sa.Start.ToString("dd.MM.yyyy. HH:mm"))
                    {
                        Dates.Remove(ds);
                        break;
                    }
                }
            }
            foreach (ScheduledAppointment sa in ScheduledAppointments)
            {
                if (sa.Patient.Id == app.LoggedInUser.Id)
                {
                    for (int i = 7; i < 10; i++)
                    {
                        foreach (DoctorsShift ds in Dates)
                        {
                            if (ds.Time == sa.Start.ToString("dd.MM.yyyy. 0" + i + ":mm"))
                            {
                                Dates.Remove(ds);
                                break;
                            }
                        }
                        
                    }
                    for (int i = 10; i < 21; i++)
                    {
                        foreach (DoctorsShift ds in Dates)
                        {
                            if (ds.Time == sa.Start.ToString("dd.MM.yyyy. " + i + ":mm"))
                            {
                                Dates.Remove(ds);
                                break;
                            }
                        }
                    }
                }
            }
            List<Doctor> doctors = new List<Doctor>(app.DoctorController.GetAll());
            foreach (DoctorsShift dShift in Dates)
            {
                string[] parts = dShift.Time.Split(" ");
                parts = parts[1].Split(":");
                int num = Convert.ToInt32(parts[0]);
                foreach (Doctor d in doctors)
                {
                    if (num < 14 && d.Shift == Shift.FIRST)
                    {
                        dShift.Doctor = d.Name + " " + d.Surname;
                        break;
                    }
                    else if (num >= 14 && d.Shift == Shift.SECOND)
                    {
                        dShift.Doctor = d.Name + " " + d.Surname;
                        break;
                    }
                }
            }

            list1.ItemsSource = Dates;

        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void createAppointment(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            DoctorsShift dShift;
            string dat;
            DateTime date;
            DateTime startDate;
            if (timeCmbBox.SelectedItem == null)
            {
                dShift = (DoctorsShift)list1.SelectedItem;
                date = Convert.ToDateTime(dShift.Time);
                startDate = date;
            }
            else
            {
                dat = ((DateTime)datePicker.SelectedDate).ToString("dd.MM.yyyy.") + " " + (string)timeCmbBox.Text;
                date = Convert.ToDateTime(dat);
                startDate = date;
            }
            DateTime endDate = date.AddHours(1);Doctor doctor = app.DoctorController.GetDoctorByShift(startDate.Hour);
            var scheduledAppointment = new ScheduledAppointment(startDate, endDate, AppointmentType.REGULAR_APPOINTMENT, app.LoggedInUser.Id, doctor.Id, doctor.Room.Id);
            scheduledAppointment = app.ScheduledAppointmentController.Create(scheduledAppointment);
            this.Close();
        }

        private void changeList(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<DoctorsShift> novo = new ObservableCollection<DoctorsShift>();
            string value = (string)timeComboBox.SelectedItem;
            foreach (DoctorsShift ds in Dates)
            {
                if (ds.Time.Contains(value))
                {
                    novo.Add(ds);
                }
            }
            list1.ItemsSource = novo;
            if (value.Equals(" /"))
            {
                list1.ItemsSource = Dates;
            }
            Ok.IsEnabled = false;
        }

        private void selectedDate(object sender, SelectionChangedEventArgs e)
        {
            timeCmbBox.SelectedItem = null;
            Ok.IsEnabled = false;
            timeCmbBox.IsEnabled = true;
            string[] time = {"07:00", "08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };
            List<string> times = new List<string>();
            foreach (string s in time)
            {
                times.Add(s);
            }
            var app = Application.Current as App;
            DateTime dat = (DateTime)datePicker.SelectedDate;
            if (DateTime.Compare(dat.Date, DateTime.Now.Date) < 0)
            {
                timeCmbBox.IsEnabled = false;
                return;
            }
            foreach (ScheduledAppointment sa in ScheduledAppointments)
            {
                if (app.LoggedInUser.Id == sa.Patient.Id)
                {
                    continue;
                }
                if (sa.Start.ToString("dd.MM.yyyy.").Contains(dat.ToString("dd.MM.yyyy.")))
                {
                    times.Remove(sa.Start.ToString("HH:mm"));
                }
            }
            foreach (ScheduledAppointment sa in ScheduledAppointments)
            {
                if (app.LoggedInUser.Id == sa.Patient.Id && sa.Start.ToString("dd.MM.yyyy.").Contains(dat.ToString("dd.MM.yyyy.")))
                {
                    timeCmbBox.IsEnabled = false;
                }
            }

            timeCmbBox.ItemsSource = times;
        }

        private void selectedTime(object sender, SelectionChangedEventArgs e)
        {
            Ok.IsEnabled = true;
        }

        private void chosenItem(object sender, SelectionChangedEventArgs e)
        {
            Ok.IsEnabled = true;
            if (list1.SelectedItem != null)
            {
                timeCmbBox.SelectedItem = null;
            }
        }

        private void chosenDate(object sender, EventArgs e)
        {
            list1.SelectedItem = null;
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                System.Windows.Controls.Primitives.DatePickerTextBox datePickerTextBox = FindVisualChild<System.Windows.Controls.Primitives.DatePickerTextBox>(datePicker);
                if (datePickerTextBox != null)
                {
                    ContentControl watermark = datePickerTextBox.Template.FindName("PART_Watermark", datePickerTextBox) as ContentControl;
                    if (watermark != null)
                    {
                        watermark.Content = "Odaberi datum";
                    }
                }
            }
        }



        private T FindVisualChild<T>(DependencyObject depencencyObject) where T : DependencyObject
        {
            if (depencencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depencencyObject); ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depencencyObject, i);
                    T result = (child as T) ?? FindVisualChild<T>(child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
