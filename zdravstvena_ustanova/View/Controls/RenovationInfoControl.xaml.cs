﻿using System;
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
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for RenovationInfoControl.xaml
    /// </summary>
    public partial class RenovationInfoControl : UserControl
    {
        public RoomCalendarOverview RoomCalendarOverview { get; set; }

        public RenovationInfoControl(RoomCalendarOverview roomCalendarOverview)
        {
            RoomCalendarOverview = roomCalendarOverview;
            InitializeComponent();
        }
    }
}
