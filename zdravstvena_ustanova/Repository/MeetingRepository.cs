using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class MeetingRepository : IMeetingRepository
    {
        private const string NOT_FOUND_ERROR = "MEETINGS NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _meetingsMaxId;

        public MeetingRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _meetingsMaxId = GetMaxId(GetAll());
        }
        
        private long GetMaxId(IEnumerable<Meeting> meetings)
        {
            return meetings.Count() == 0 ? 0 : meetings.Max(meeting => meeting.Id);
        }

        public IEnumerable<Meeting> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToMeeting)
                .ToList();
        }

        public Meeting Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(meeting => meeting.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Meeting Create(Meeting meeting)
        {
            meeting.Id = ++_meetingsMaxId;
            AppendLineToFile(_path, MeetingToCSVFormat(meeting));
            return meeting;
        }

        public bool Update(Meeting meeting)
        {
            var meetings = GetAll();

            foreach (Meeting m in meetings)
            {
                if (m.Id == meeting.Id)
                {
                    m.Time = meeting.Time;
                    m.Room = meeting.Room;
                    m.Topic = meeting.Topic;
                    m.Participants = meeting.Participants;
                    WriteLinesToFile(_path, MeetingsToCSVFormat((List<Meeting>) meetings));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long meetingId)
        {
            var meetings = (List<Meeting>)GetAll();

            foreach (Meeting m in meetings)
            {
                if (m.Id == meetingId)
                {
                    meetings.Remove(m);
                    WriteLinesToFile(_path, MeetingsToCSVFormat((List<Meeting>) meetings));
                    return true;
                }
            }
            return false;
        }
        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private string MeetingToCSVFormat(Meeting meeting)
        {
            int count = meeting.Participants.Count;
            string participants = "";
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                    participants = meeting.Participants[i].Id.ToString();
                else
                    participants = string.Join(_delimiter, participants, meeting.Participants[i].Id);
            }
            return string.Join(_delimiter,
                meeting.Id,
                meeting.Time,
                meeting.Room.Id,
                meeting.Topic,
                count,
                participants
                );
        }
        private List<string> MeetingsToCSVFormat(List<Meeting> meetings)
        {
            List<string> lines = new List<string>();

            foreach (Meeting meeting in meetings)
            {
                lines.Add(MeetingToCSVFormat(meeting));
            }
            return lines;
        }

        private Meeting CSVFormatToMeeting(string meetingCSVFormat)
        {
            var tokens = meetingCSVFormat.Split(_delimiter.ToCharArray());

            List<Account> participants = new List<Account>();
            for (int i = 5; i < 5 + int.Parse(tokens[4]); i++)
            {
                var participant = new Account(int.Parse(tokens[i]));
                participants.Add(participant);
            }

            return new Meeting(
                long.Parse(tokens[0]),
                Convert.ToDateTime(tokens[1]),
                long.Parse(tokens[2]),
                tokens[3],
                participants
                );
        }

    }
}
