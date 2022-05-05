using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace zdravstvena_ustanova.View
{
    public partial class ChangeAppointment : Window
    {
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public ChangeAppointment(ScheduledAppointment sa)
        {
            InitializeComponent();
            ScheduledAppointment = sa;
            List<string> dates = new List<string>();
            dates.Add(ScheduledAppointment.Start.AddDays(1).ToString("dd.MM.yyyy."));
            dates.Add(ScheduledAppointment.Start.AddDays(2).ToString("dd.MM.yyyy."));
            dates.Add(ScheduledAppointment.Start.AddDays(3).ToString("dd.MM.yyyy."));
            dateComboBox.ItemsSource = dates;
        }

        private void goToManageAppointment(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void selectedDate(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            timeComboBox.SelectedItem = null;
            if(timeComboBox.SelectedItem == null)
            {
                yes.IsEnabled = false;
            }
            timeComboBox.IsEnabled = true;
            string[] time = {"07:00", "08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };
            List<string> times = new List<string>();
            foreach(string s in time)
            {
                times.Add(s);
            }
            var app = Application.Current as App;
            List<ScheduledAppointment> sa = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            string dat = (string)dateComboBox.SelectedItem;
            foreach (ScheduledAppointment sapp in sa)
            {
                if(ScheduledAppointment.Patient.Id == sapp.Patient.Id)
                {
                    continue;
                }
                if (sapp.Start.ToString("dd.MM.yyyy.").Contains(dat))
                {
                    times.Remove(sapp.Start.ToString("HH:mm"));
                }
            }
            foreach (ScheduledAppointment sapp in sa)
            {
                if (ScheduledAppointment.Patient.Id == sapp.Patient.Id && sapp.Start.ToString("dd.MM.yyyy.").Contains(dat))
                {
                    timeComboBox.IsEnabled = false;
                }
            }

            timeComboBox.ItemsSource = times;

        }

        private void timeSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            yes.IsEnabled = true;
        }

        private void changeAppointment(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            ScheduledAppointment.Start = Convert.ToDateTime((string)dateComboBox.SelectedItem + " " + (string)timeComboBox.SelectedItem);
            ScheduledAppointment.End = ScheduledAppointment.Start.AddHours(1);

            List<Doctor>  doctors = new List<Doctor>(app.DoctorController.GetAll());
            long docId = 0;
            long docRoom = 0;
            foreach (Doctor d in doctors)
            {
                if (ScheduledAppointment.Start.Hour < 14 && d.Shift == Shift.FIRST)
                {
                    docId = d.Id;
                    docRoom = d.Room.Id;
                    break;
                }
                else if (ScheduledAppointment.Start.Hour >= 14 && d.Shift == Shift.SECOND)
                {
                    docId = d.Id;
                    docRoom = d.Room.Id;
                    break;
                }
            }
            ScheduledAppointment.Doctor.Id = docId;
            ScheduledAppointment.Room.Id = docRoom;
            ScheduledAppointment.Doctor = app.DoctorController.GetById(docId);
            ScheduledAppointment.Room = app.RoomController.GetById(docRoom);
            app.ScheduledAppointmentController.Update(ScheduledAppointment);
            this.Close();
        }
    }
}
