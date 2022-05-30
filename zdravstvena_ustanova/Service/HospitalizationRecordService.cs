using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class HospitalizationRecordService
    {
        private readonly IHospitalizationRecordRepository _hospitalizationRecordRepository;
        private readonly IRoomRepository _roomRepository;

        public HospitalizationRecordService(IHospitalizationRecordRepository hospitalizationRecordRepository, IRoomRepository roomRepository)
        {
            _hospitalizationRecordRepository = hospitalizationRecordRepository;
            _roomRepository = roomRepository;
        }

        public IEnumerable<HospitalizationRecord> GetAll()
        {
            var hospitalizationRecords = _hospitalizationRecordRepository.GetAll();
            var rooms = _roomRepository.GetAll();
            BindHospitalizationRecordsWithRooms(hospitalizationRecords, rooms);
            return hospitalizationRecords;
        }

        private void BindHospitalizationRecordsWithRooms(IEnumerable<HospitalizationRecord> hospitalizationRecords, IEnumerable<Room> rooms)
        {
            foreach (HospitalizationRecord hr in hospitalizationRecords)
            {
                BindHospitalizationRecordWithRoom(hr, rooms);
            }
        }

        public HospitalizationRecord GetById(long id)
        {
            var rooms = _roomRepository.GetAll();
            var hospitalizationRecord = _hospitalizationRecordRepository.Get(id);
            BindHospitalizationRecordWithRoom(hospitalizationRecord, rooms);
            return hospitalizationRecord;
        }

        private void BindHospitalizationRecordWithRoom(HospitalizationRecord hospitalizationRecord, IEnumerable<Room> rooms)
        {
            foreach (Room r in rooms)
            {
                if (hospitalizationRecord.Room.Id == r.Id)
                {
                    hospitalizationRecord.Room = r;
                    break;
                }
            }
        }

        public HospitalizationRecord Create(HospitalizationRecord hospitalizationRecord)
        {
            return _hospitalizationRecordRepository.Create(hospitalizationRecord);
        }
        public bool Update(HospitalizationRecord hospitalizationRecord)
        {
            return _hospitalizationRecordRepository.Update(hospitalizationRecord);
        }
        public bool Delete(long hospitalizationRecordId)
        {
            return _hospitalizationRecordRepository.Delete(hospitalizationRecordId);
        }
    }
}
