using Model;
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

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for RemoveItemFromRoom.xaml
    /// </summary>
    public partial class RemoveItemFromRoom : UserControl
    {
        DataGrid RoomItemsDataGrid { get; set; }
        
        public RemoveItemFromRoom(Room room, DataGrid roomItemsDataGrid)
        {
            InitializeComponent();
            DataContext = this;
            RoomItemsDataGrid = roomItemsDataGrid;
        }
    }
}
