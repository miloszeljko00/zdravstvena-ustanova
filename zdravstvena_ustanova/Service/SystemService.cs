using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using Model.Enums;
using System.Threading;

namespace Service
{
    public class SystemService
    {
        private readonly ScheduledItemTransferRepository _scheduledItemTransferRepository;
        private readonly StoredItemRepository _storedItemRepository;
        private Timer timer;
        public SystemService(ScheduledItemTransferRepository scheduledItemTransferRepository, StoredItemRepository storedItemRepository)
        {
            _scheduledItemTransferRepository = scheduledItemTransferRepository;
            _storedItemRepository = storedItemRepository;
        }

        public async void StartCheckingForScheduledItemTransfers(int numberOfSecondsBetweenTwoChecks)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(numberOfSecondsBetweenTwoChecks));

            while (await timer.WaitForNextTickAsync())
            {
                CheckForScheduledItemTransfers();
            }
        }

        private void CheckForScheduledItemTransfers()
        {
            List<ScheduledItemTransfer> scheduledItemTransfers = (List<ScheduledItemTransfer>)_scheduledItemTransferRepository.GetAll();

            foreach(ScheduledItemTransfer scheduledItemTransfer in scheduledItemTransfers)
            {
                if(scheduledItemTransfer.TransferDate.CompareTo(DateTime.Now) <= 0)
                {
                    ExecuteScheduledItemTransfer(scheduledItemTransfer);
                }
            }
        }

        private void ExecuteScheduledItemTransfer(ScheduledItemTransfer scheduledItemTransfer)
        {
            List<StoredItem> storedItems = (List<StoredItem>)_storedItemRepository.GetAll();

            foreach(StoredItem storedItem in storedItems)
            {
                if(storedItem.Item.Id == scheduledItemTransfer.Item.Id)
                {
                    if(storedItem.StorageType == scheduledItemTransfer.SourceStorageType)
                    {
                        if (storedItem.StorageType == StorageType.ROOM)
                        {
                            if(storedItem.Room.Id == scheduledItemTransfer.SourceRoom.Id)
                            {
                                if(scheduledItemTransfer.ItemsForTransfer == storedItem.Quantity)
                                {
                                    _storedItemRepository.Delete(storedItem.Id);
                                    CreateNewStoredItem(scheduledItemTransfer);
                                    _scheduledItemTransferRepository.Delete(scheduledItemTransfer.Id);
                                }
                                else
                                {
                                    storedItem.Quantity -= scheduledItemTransfer.ItemsForTransfer;
                                    _storedItemRepository.Update(storedItem);
                                    CreateNewStoredItem(scheduledItemTransfer);
                                    _scheduledItemTransferRepository.Delete(scheduledItemTransfer.Id);
                                }
                            }
                        }
                        else if (storedItem.StorageType == StorageType.WAREHOUSE)
                        {
                            if (storedItem.Warehouse.Id == scheduledItemTransfer.SourceWarehouse.Id)
                            {
                                if (scheduledItemTransfer.ItemsForTransfer == storedItem.Quantity)
                                {
                                    _storedItemRepository.Delete(storedItem.Id);
                                    CreateNewStoredItem(scheduledItemTransfer);
                                    _scheduledItemTransferRepository.Delete(scheduledItemTransfer.Id);
                                }
                                else
                                {
                                    storedItem.Quantity -= scheduledItemTransfer.ItemsForTransfer;
                                    _storedItemRepository.Update(storedItem);
                                    CreateNewStoredItem(scheduledItemTransfer);
                                    _scheduledItemTransferRepository.Delete(scheduledItemTransfer.Id);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateNewStoredItem(ScheduledItemTransfer scheduledItemTransfer)
        {
            if (scheduledItemTransfer.DestinationStorageType == StorageType.ROOM)
            {
                var transferedItemStored = new StoredItem(-1, scheduledItemTransfer.Item.Id, scheduledItemTransfer.ItemsForTransfer,
                    scheduledItemTransfer.DestinationStorageType, scheduledItemTransfer.DestinationRoom.Id);

                _storedItemRepository.Create(transferedItemStored);
            }
            else if (scheduledItemTransfer.DestinationStorageType == StorageType.WAREHOUSE)
            {
                var transferedItemStored = new StoredItem(-1, scheduledItemTransfer.Item.Id, scheduledItemTransfer.ItemsForTransfer,
                    scheduledItemTransfer.DestinationStorageType, scheduledItemTransfer.DestinationWarehouse.Id);

                _storedItemRepository.Create(transferedItemStored);
            }
        }
    }
}
