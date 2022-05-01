﻿using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace zdravstvena_ustanova.View
{
    public partial class TimePriority : Window
    {
        public ObservableCollection<string> dates;
        public TimePriority()
        {
            InitializeComponent();
            string[] times = {" /", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00",
                                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00" };

            timeComboBox.ItemsSource = times;
            DateTime today = DateTime.Now;
            today = today.AddMinutes(-today.Minute);
            today = today.AddHours(1);
            if(today.Hour >= 20){
                today = new DateTime(today.Year, today.Month, today.Day + 1, 7, 0, 0);
            }
            if (today.Hour <= 7)
            {
                today = new DateTime(today.Year, today.Month, today.Day, 7, 0, 0);
            }
            int days = DateTime.DaysInMonth(2022, DateTime.Now.Month);
            int to = today.Day + 4;
            if(to > days) { to -= days; }
            dates = new ObservableCollection<string>();
            while (true)
            {
                if(today.Hour == 21) { today = today.AddDays(1); today = today.AddHours(-14); }
                if (today.Day == to) break;
                dates.Add(today.ToString("dd.MM.yyyy. HH:mm"));
                today = today.AddHours(1);
            }
            var app = Application.Current as App;
            List<ScheduledAppointment> sa = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            foreach(ScheduledAppointment sapp in sa)
            {
                dates.Remove(sapp.Start.ToString("dd.MM.yyyy. HH:mm"));
            }
            foreach (ScheduledAppointment sapp in sa)
            {
                if (sapp.Patient.Id == app.LoggedInUser.Id)
                {
                    for (int i = 7; i < 10; i++)
                    {
                        dates.Remove(sapp.Start.ToString("dd.MM.yyyy. 0" + i + ":mm"));
                    }
                    for (int i=10; i < 21; i++)
                    {
                        dates.Remove(sapp.Start.ToString("dd.MM.yyyy. " + i + ":mm"));
                    }
                }
            }
            list1.ItemsSource = dates;

        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void createAppointment(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            string dat;
            DateTime date;
            DateTime startDate;
            if (timeCmbBox.SelectedItem == null)
            {
                dat = (string)list1.SelectedItem;
                date = Convert.ToDateTime(dat);
                startDate = date;
            }
            else
            {
                dat = ((DateTime)datePicker.SelectedDate).ToString("dd.MM.yyyy.") + " " + (string)timeCmbBox.Text;
                date = Convert.ToDateTime(dat);
                startDate = date;
            }
            DateTime endDate = date.AddHours(1);
            long docId = 0;
            long docRoom = 0;
            List<Doctor> doctors = new List<Doctor>(app.DoctorController.GetAll());
            foreach (Doctor d in doctors)
            {
                if (startDate.Hour < 14 && d.Shift == Shift.FIRST)
                {
                    docId = d.Id;
                    docRoom = d.Room.Id;
                    break;
                }
                else if(startDate.Hour >= 14 && d.Shift == Shift.SECOND)
                {
                    docId = d.Id;
                    docRoom = d.Room.Id;
                    break;
                }
            }

            var scheduledAppointment = new ScheduledAppointment(startDate, endDate, AppointmentType.REGULAR_APPOINTMENT, app.LoggedInUser.Id, docId, docRoom);
            scheduledAppointment = app.ScheduledAppointmentController.Create(scheduledAppointment);
            scheduledAppointment = app.ScheduledAppointmentController.GetById(scheduledAppointment.Id);
            this.Close();
        }

        private void changeList(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<string> novo = new ObservableCollection<string>();
            string value = (string)timeComboBox.SelectedItem;
            foreach (string s in dates)
            {
                if (s.Contains(value))
                {
                    novo.Add(s);
                }
            }
            list1.ItemsSource = novo;
            if (value.Equals(" /"))
            {
                list1.ItemsSource = dates;
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
            List<ScheduledAppointment> sa = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            DateTime dat = (DateTime)datePicker.SelectedDate;
            if(DateTime.Compare(dat.Date, DateTime.Now.Date) < 0)
            {
                timeCmbBox.IsEnabled = false;
                return;
            }
            foreach (ScheduledAppointment sapp in sa)
            {
                if (app.LoggedInUser.Id == sapp.Patient.Id)
                {
                    continue;
                }
                if (sapp.Start.ToString("dd.MM.yyyy.").Contains(dat.ToString("dd.MM.yyyy.")))
                {
                    times.Remove(sapp.Start.ToString("HH:mm"));
                }
            }
            foreach (ScheduledAppointment sapp in sa)
            {
                if (app.LoggedInUser.Id == sapp.Patient.Id && sapp.Start.ToString("dd.MM.yyyy.").Contains(dat.ToString("dd.MM.yyyy.")))
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
    }
}