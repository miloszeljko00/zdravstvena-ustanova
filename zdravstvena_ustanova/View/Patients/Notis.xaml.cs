﻿using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace zdravstvena_ustanova.View
{
    public partial class Notis : UserControl
    {
        public ObservableCollection<PrescribedMedicine> pm { get; set; }
        public Notis()
        {
            InitializeComponent();
            pm = new ObservableCollection<PrescribedMedicine>();
            var app = Application.Current as App;
            List<MedicalExamination> me = new List<MedicalExamination>(app.MedicalExaminationController.GetAll());
            List<ScheduledAppointment> sa = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            foreach (MedicalExamination medEx in me)
            {
                foreach (ScheduledAppointment scApp in sa)
                {
                    if (app.LoggedInUser.Id == scApp.Patient.Id && medEx.ScheduledAppointment.Id == scApp.Id)
                    {
                        foreach (PrescribedMedicine preMed in medEx.PrescribedMedicine)
                        {
                            pm.Add(preMed);
                        }
                    }
            }
            }
            pm.Add(new PrescribedMedicine(1, 4, 6, new DateTime(2022, 6, 5), "dodat lek 1", new Medication(1,"lek1")));
            pm.Add(new PrescribedMedicine(1, 3, 8, new DateTime(2022, 6, 7), "dodat lek 2", new Medication(2, "lek2")));
            pm.Add(new PrescribedMedicine(1, 2, 12, new DateTime(2022, 6, 3), "dodat lek 3", new Medication(3, "lek3")));
            pm.Add(new PrescribedMedicine(1, 1, 24, new DateTime(2022, 7, 5), "dodat lek 4", new Medication(4, "lek4")));
            therapyList.ItemsSource = pm;
        }

        private void entered(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space && therapyList.SelectedIndex != -1)
            {
                PrescribedMedicineDetails pmd = new PrescribedMedicineDetails((PrescribedMedicine)therapyList.SelectedItem);
                pmd.ShowDialog();
            }
        }

        private void pdfTherapies(object sender, RoutedEventArgs e)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            graphics.DrawString("Raspored terapija na sedmicnom nivou (" + DateTime.Now.Day.ToString()  + "." + DateTime.Now.Month.ToString() + " - " + DateTime.Now.AddDays(7).Day.ToString() + "." + DateTime.Now.AddDays(7).Month.ToString() +")", font, PdfBrushes.Black, new PointF(0, 0));
            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Lek");
            dataTable.Columns.Add("Broj doza dnevno");
            dataTable.Columns.Add("Razmak izmedju doza (u satima)");
            dataTable.Columns.Add("Datum do kog se lek koristi");
            dataTable.Columns.Add("Opis leka");
            foreach (PrescribedMedicine p in pm)
            {
                if(DateTime.Compare(DateTime.Now, p.EndDate) < 0 && DateTime.Compare(DateTime.Now.AddDays(7), p.EndDate) > 0)
                    dataTable.Rows.Add(new object[] {p.Medication.Name, p.TimesPerDay, p.OnHours, p.EndDate.Day.ToString() + "." + p.EndDate.Month.ToString() + "." + p.EndDate.Year.ToString(), p.Description});
            }
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page, new PointF(0, 30));
            doc.Save("Therapies.pdf");
            doc.Close(true);
        }
    }
}
