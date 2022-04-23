using Model.Enums;
using System;

namespace Model
{
    public class HospitalizationRecord
    {
        public long Id { get; set; }
        public string Cause { get; set; }
        public DateTime Admission { get; set; }
        public DateTime Release { get; set; }
        public ReleaseKind ReleaseKind { get; set; }

        public Room Room { get; set; }

        public HospitalizationRecord(long id, string cause, DateTime admission, DateTime release, ReleaseKind releaseKind, Room room)
        {
            Id = id;
            Cause = cause;
            Admission = admission;
            Release = release;
            ReleaseKind = releaseKind;
            Room = room;
        }

        public HospitalizationRecord(long id, string cause, DateTime admission, DateTime release, ReleaseKind releaseKind, long roomId)
        {
            Id = id;
            Cause = cause;
            Admission = admission;
            Release = release;
            ReleaseKind = releaseKind;
            Room = new Room(roomId);
        }

        public HospitalizationRecord(long id)
        {
            Id = id;
        }
    }
}