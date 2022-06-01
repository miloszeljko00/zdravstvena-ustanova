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

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for HolidayRequestPage.xaml
    /// </summary>
    public partial class HolidayRequestPage : Page
    {
        public ObservableCollection<HolidayRequest> HolidayRequests { get; set; }

        public ICollectionView RequestView { get; set; }

        private HomePagePatients _homePagePatients;
        public HolidayRequestPage(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            _homePagePatients = hpp;
            var app = Application.Current as App;
            HolidayRequests = new ObservableCollection<HolidayRequest>(app.HolidayRequestController.GetAll());
            RequestView = new CollectionViewSource { Source = HolidayRequests }.View;
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _homePagePatients.SecretaryFrame.Content = new SchedulePage(_homePagePatients);

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var editIcon = (Image)e.OriginalSource;
            var dataContext = editIcon.DataContext;
            HolidayRequest hr = dataContext as HolidayRequest;
            if (hr != null)
            {
                _homePagePatients.SecretaryFrame.Content =  new EditHolidayRequestPage(_homePagePatients, hr);
            }
        }
    }
}
