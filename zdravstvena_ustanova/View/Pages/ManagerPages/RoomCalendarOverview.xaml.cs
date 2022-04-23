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
using zdravstvena_ustanova.View.Controls.RoomsCalendar;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for RoomCalendarOverview.xaml
    /// </summary>
    public partial class RoomCalendarOverview : Page
    {
        public RoomCalendarOverview()
        {
            InitializeComponent();
            Content = new RoomsCalendarControl();
        }
    }
}
