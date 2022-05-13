﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System.Threading;

namespace zdravstvena_ustanova.Service
{
    public class SystemService
    {
        private readonly ScheduledItemTransferRepository _scheduledItemTransferRepository;
        private readonly StoredItemRepository _storedItemRepository;
        private readonly RenovationAppointmentRepository _renovationAppointmentRepository;
        private readonly RoomRepository _roomRepository;
        private readonly RoomRepository _roomUnderRenovationRepository;
        public SystemService(ScheduledItemTransferRepository scheduledItemTransferRepository, StoredItemRepository storedItemRepository,
            RenovationAppointmentRepository renovationAppointmentRepository, RoomRepository roomRepository, RoomRepository roomUnderRenovationRepository)
        {
            _scheduledItemTransferRepository = scheduledItemTransferRepository;
            _storedItemRepository = storedItemRepository;
            _renovationAppointmentRepository = renovationAppointmentRepository;
            _roomRepository = roomRepository;
            _roomUnderRenovationRepository = roomUnderRenovationRepository;
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
        public async void StartCheckingForRenovationAppointments(int numberOfSecondsBetweenTwoChecks)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(numberOfSecondsBetweenTwoChecks));

            while (await timer.WaitForNextTickAsync())
            {
                CheckForRenovationAppointments();
            }
        }

        private void CheckForRenovationAppointments()
        {
            List<RenovationAppointment> renovationAppointments = (List<RenovationAppointment>)_renovationAppointmentRepository.GetAll();

            foreach (RenovationAppointment renovationAppointment in renovationAppointments)
            {
                if (renovationAppointment.EndDate.CompareTo(DateTime.Now) <= 0)
                {
                    ExecuteRenovation(renovationAppointment);
                }
            }
        }

        private void ExecuteRenovation(RenovationAppointment renovationAppointment)
        {
            if(renovationAppointment.RenovationType.Id == 2)
            {
                var secondRoom = _roomUnderRenovationRepository.Get(renovationAppointment.SecondRoom.Id);
                _roomUnderRenovationRepository.Delete(secondRoom.Id);
                renovationAppointment.SecondRoom = _roomRepository.Create(secondRoom);

                MoveStoredItemsFromFirstRoomNewRoom(renovationAppointment);
                MoveStoredItemsFromSecondRoomToNewRoom(renovationAppointment);

                _roomRepository.Delete(renovationAppointment.Room.Id);
                _roomRepository.Delete(renovationAppointment.FirstRoom.Id);

                _renovationAppointmentRepository.Delete(renovationAppointment.Id);
                var renovationAppointments = (List<RenovationAppointment>)_renovationAppointmentRepository.GetAll();
                renovationAppointments = renovationAppointments.FindAll(ra => ra.RenovationType.Id == 1);
                renovationAppointments = renovationAppointments.FindAll(ra => ra.Room.Id == renovationAppointment.FirstRoom.Id);
                renovationAppointments = renovationAppointments.FindAll(ra => ra.StartDate.CompareTo(renovationAppointment.StartDate) >= 0);
                foreach(var ra in renovationAppointments)
                {
                    _renovationAppointmentRepository.Delete(ra.Id);
                }
            }
            else if(renovationAppointment.RenovationType.Id == 3)
            {
                var firstRoom = _roomUnderRenovationRepository.Get(renovationAppointment.FirstRoom.Id);
                _roomUnderRenovationRepository.Delete(firstRoom.Id);
                renovationAppointment.FirstRoom = _roomRepository.Create(firstRoom);

                var secondRoom = _roomUnderRenovationRepository.Get(renovationAppointment.SecondRoom.Id);
                _roomUnderRenovationRepository.Delete(secondRoom.Id);
                renovationAppointment.SecondRoom = _roomRepository.Create(secondRoom);

                MoveStoredItemsToFirstNewRoom(renovationAppointment);

                _roomRepository.Delete(renovationAppointment.Room.Id);

                _renovationAppointmentRepository.Delete(renovationAppointment.Id);
            }
        }

        private void MoveStoredItemsToFirstNewRoom(RenovationAppointment renovationAppointment)
        {
            var storedItems = (List<StoredItem>)_storedItemRepository.GetAll();

            storedItems = storedItems.FindAll(storedItem => storedItem.StorageType == StorageType.ROOM);

            storedItems = storedItems.FindAll(storedItem => storedItem.Room.Id == renovationAppointment.Room.Id);

            foreach (var storedItem in storedItems)
            {
                storedItem.Room = renovationAppointment.FirstRoom;
                _storedItemRepository.Update(storedItem);
            }
        }

        private void MoveStoredItemsFromSecondRoomToNewRoom(RenovationAppointment renovationAppointment)
        {
            var storedItems = (List<StoredItem>)_storedItemRepository.GetAll();

            storedItems = storedItems.FindAll(storedItem => storedItem.StorageType == StorageType.ROOM);
            var storedItemsFromSecondRoom = storedItems.FindAll(storedItem => storedItem.Room.Id == renovationAppointment.FirstRoom.Id);

            foreach (var storedItem in storedItemsFromSecondRoom)
            {
                storedItem.Room = renovationAppointment.SecondRoom;
                _storedItemRepository.Update(storedItem);
            }
        }

        private void MoveStoredItemsFromFirstRoomNewRoom(RenovationAppointment renovationAppointment)
        {
            var storedItems = (List<StoredItem>)_storedItemRepository.GetAll();

            storedItems = storedItems.FindAll(storedItem => storedItem.StorageType == StorageType.ROOM);
            var storedItemsFromFirstRoom = storedItems.FindAll(storedItem => storedItem.Room.Id == renovationAppointment.Room.Id);

            foreach (var storedItem in storedItemsFromFirstRoom)
            {
                storedItem.Room = renovationAppointment.FirstRoom;
                _storedItemRepository.Update(storedItem);
            }
        }
    }
}
