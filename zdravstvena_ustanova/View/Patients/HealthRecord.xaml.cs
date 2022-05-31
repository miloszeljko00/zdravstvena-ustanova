using System;
using System.Collections.Generic;
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

namespace zdravstvena_ustanova.View
{
    public class MyItem
    {
        public string Kategorija { get; set; }
        public string Podatak { get; set; }
        public string Promena { get; set; }
    }
    public class AnotherItem
    {
        public string Rbr { get; set; }
        public string Our { get; set; }
        public string Mesto { get; set; }
        public string Broj { get; set; }
        public string Sifra { get; set; }
        public string Posao { get; set; }
        public string Osiz { get; set; }

    }
    public class AndAnotherItem
    {
        public string Datum { get; set; }
        public string Tok { get; set; }
        public string Terapija { get; set; }

    }
    public partial class HealthRecordPatient : UserControl
    {
        public HealthRecordPatient()
        {
            InitializeComponent();
            List<MyItem> items = new List<MyItem>();
            List<AnotherItem> items1 = new List<AnotherItem>();
            List<AndAnotherItem> items2 = new List<AndAnotherItem>();
            items.Add(new MyItem { Kategorija = "Radnici", Podatak = "Prvi podatak", Promena = "Prva promena ako postoji" });
            items.Add(new MyItem { Kategorija = "Clanovi porodice radnika", Podatak = "Drugi podatak", Promena = "Druga promena ako postoji" });
            items.Add(new MyItem { Kategorija = "...", Podatak = "Treci podatak", Promena = "Treca promena ako postoji" });
            items1.Add(new AnotherItem { Rbr = "1", Our = " ", Broj = " ", Mesto = " ", Sifra = " ", Posao = " ", Osiz = " " });
            items1.Add(new AnotherItem { Rbr = "2", Our = " ", Broj = " ", Mesto = " ", Sifra = " ", Posao = " ", Osiz = " " });
            items1.Add(new AnotherItem { Rbr = "3", Our = " ", Broj = " ", Mesto = " ", Sifra = " ", Posao = " ", Osiz = " " });
            items1.Add(new AnotherItem { Rbr = "4", Our = " ", Broj = " ", Mesto = " ", Sifra = " ", Posao = " ", Osiz = " " });
            items1.Add(new AnotherItem { Rbr = "5", Our = " ", Broj = " ", Mesto = " ", Sifra = " ", Posao = " ", Osiz = " " });
            items2.Add(new AndAnotherItem { Datum = "01.06.2022.", Tok = "Operacija uspela", Terapija = "Prepisana fizicka terapija" });
            items2.Add(new AndAnotherItem { Datum = "02.06.2022.", Tok = "Operacija obavljena, preporucen nadzor pacijenta", Terapija = "Prepisana hemoterapija" });
            items2.Add(new AndAnotherItem { Datum = "03.06.2022.", Tok = "Pacijent pregledan i postavljena dijagnoza", Terapija = "Prepisana kucna terapija" });
            list.ItemsSource = items;
            list1.ItemsSource = items1;
            list2.ItemsSource = items2;
        }
    }
}
