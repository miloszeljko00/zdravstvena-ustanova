using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface
{
    public class ISystemService
    {
        public async void StartCheckingForScheduledItemTransfers(int numberOfSecondsBetweenTwoChecks)
        {
        }

        private void CheckForScheduledItemTransfers()
        {
        }

        private void ExecuteScheduledItemTransfer(ScheduledItemTransfer scheduledItemTransfer)
        {
        }

        private void CreateNewStoredItem(ScheduledItemTransfer scheduledItemTransfer)
        {
        }
        public async void StartCheckingForRenovationAppointments(int numberOfSecondsBetweenTwoChecks)
        {
        }

        private void CheckForRenovationAppointments()
        {
        }

        private void ExecuteRenovation(RenovationAppointment renovationAppointment)
        {
        }

        private void ExecuteSplitRenovation(RenovationAppointment renovationAppointment)
        {
        }

        private void ExecuteMergeRenovation(RenovationAppointment renovationAppointment)
        {
        }

        private void DeleteRenovationAppointmentForMergedRoom(RenovationAppointment renovationAppointment)
        {
        }

        private void MoveStoredItemsToFirstNewRoom(RenovationAppointment renovationAppointment)
        {
        }

        private void MoveStoredItemsFromSecondRoomToNewRoom(RenovationAppointment renovationAppointment)
        {
        }

        private void MoveStoredItemsFromFirstRoomNewRoom(RenovationAppointment renovationAppointment)
        {
        }
    }
}
