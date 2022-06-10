using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.ManagerMVVM.Model
{
    public class RoomsModel : BindableBase
    {
        private ObservableCollection<Room> _rooms;

        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set => SetProperty(ref _rooms, value);
        }
        private Room? _selectedRoom;

        public Room? SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value);
        }

        public RoomsModel(IEnumerable<Room> rooms)
        {
            Rooms = new ObservableCollection<Room>(rooms);

        }


    }
}
