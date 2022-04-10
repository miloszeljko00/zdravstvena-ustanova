using Zdravstena_ustanova.Exception;
using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Repository
{
    public class ScheduledAppointmentRepository
    {
        private const string NOT_FOUND_ERROR = "ROOM NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _scheduledAppointmentMaxId;

        public ScheduledAppointmentRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _scheduledAppointmentMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<ScheduledAppointment> scheduledAppointments)
        {
            return scheduledAppointments.Count() == 0 ? 0 : scheduledAppointments.Max(scheduledAppointment => scheduledAppointment.Id);
        }

        public IEnumerable<ScheduledAppointment> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToScheduledAppointment)
                .ToList();
        }

        public ScheduledAppointment Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(scheduledAppointment => scheduledAppointment.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public ScheduledAppointment Create(ScheduledAppointment scheduledAppointment)
        {
            scheduledAppointment.Id = ++_scheduledAppointmentMaxId;
            AppendLineToFile(_path, ScheduledAppointmentToCSVFormat(scheduledAppointment));
            return scheduledAppointment;
        }

        public void Update(ScheduledAppointment scheduledAppointment)
        {
            var scheduledAppointments = GetAll();

            foreach (ScheduledAppointment sa in scheduledAppointments)
            {
                if(sa.Id == scheduledAppointment.Id)
                {
                    sa.Start = scheduledAppointment.Start;
                    sa.End = scheduledAppointment.End;
                    sa.AppointmentType = scheduledAppointment.AppointmentType;
                    sa.PatientId = scheduledAppointment.PatientId;
                    sa.DoctorId = scheduledAppointment.DoctorId;
                    sa.RoomId = scheduledAppointment.RoomId;
                    WriteLinesToFile(_path, ScheduledAppointmentsToCSVFormat((List<ScheduledAppointment>)scheduledAppointments));
                    break;
                }
            }


        }
        public void Delete(long scheduledAppointmentId)
        {
            var scheduledAppointments = (List<ScheduledAppointment>)GetAll();

            foreach (ScheduledAppointment sa in scheduledAppointments)
            {
                if (sa.Id == scheduledAppointmentId)
                {
                    scheduledAppointments.Remove(sa);
                   break;
                }
            }


            WriteLinesToFile(_path, ScheduledAppointmentsToCSVFormat((List<ScheduledAppointment>)scheduledAppointments));
        }

        private string ScheduledAppointmentToCSVFormat(ScheduledAppointment scheduledAppointment)
        {
            return string.Join(_delimiter,
                scheduledAppointment.Start.ToString("dd.MM.yyyy HH:mm"),
                scheduledAppointment.End.ToString("dd.MM.yyyy HH:mm"),
                (int)scheduledAppointment.AppointmentType,
                scheduledAppointment.Id,
                scheduledAppointment.PatientId,
                scheduledAppointment.DoctorId,
                scheduledAppointment.RoomId
                );
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private ScheduledAppointment CSVFormatToScheduledAppointment(string scheduledAppointmentCSVFormat)
        {
            var tokens = scheduledAppointmentCSVFormat.Split(_delimiter.ToCharArray());
            var timeFormat = "dd.MM.yyyy HH:mm";
            DateTime startTime;
            DateTime endTime;

            DateTime.TryParseExact(tokens[0], timeFormat, CultureInfo.InvariantCulture
                                                , DateTimeStyles.None
                                                , out startTime);
            DateTime.TryParseExact(tokens[1], timeFormat, CultureInfo.InvariantCulture
                                                , DateTimeStyles.None
                                                , out endTime);

            return new ScheduledAppointment(
               startTime,
               endTime,
               (AppointmentType)int.Parse(tokens[2]),
               long.Parse(tokens[3]),
               long.Parse(tokens[4]),
               long.Parse(tokens[5]),
               long.Parse(tokens[6])
                );
        }

        private List<string> ScheduledAppointmentsToCSVFormat(List<ScheduledAppointment> scheduledAppointments)
        {
            List<string> lines = new List<string>();

            foreach (ScheduledAppointment scheduledAppointment in scheduledAppointments)
            {
                lines.Add(ScheduledAppointmentToCSVFormat(scheduledAppointment));
            }
            return lines;
        }
    }
}