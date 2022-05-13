using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Exception;

namespace zdravstvena_ustanova.Repository
{
    public class RenovationAppointmentRepository
    {
        private const string NOT_FOUND_ERROR = "RENOVATION APPOINTMENT NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _renovationAppointmentMaxId;

        public RenovationAppointmentRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _renovationAppointmentMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<RenovationAppointment> renovationAppointments)
        {
            return renovationAppointments.Count() == 0 ? 0 : renovationAppointments.Max(renovationAppointment => renovationAppointment.Id);
        }

        public IEnumerable<RenovationAppointment> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToRenovationAppointment)
                .ToList();
        }

        public RenovationAppointment Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(renovationAppointment => renovationAppointment.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public RenovationAppointment Create(RenovationAppointment renovationAppointment)
        {
            renovationAppointment.Id = ++_renovationAppointmentMaxId;
            AppendLineToFile(_path, RenovationAppointmentToCSVFormat(renovationAppointment));
            return renovationAppointment;
        }

        public bool Update(RenovationAppointment renovationAppointment)
        {
            var renovationAppointments = GetAll();

            foreach (RenovationAppointment ra in renovationAppointments)
            {
                if (ra.Id == renovationAppointment.Id)
                {
                    ra.StartDate = renovationAppointment.StartDate;
                    ra.EndDate = renovationAppointment.EndDate;
                    WriteLinesToFile(_path, RenovationAppointmentsToCSVFormat((List<RenovationAppointment>)renovationAppointments));
                    return true;
                }
            }
            return false;
        }
        public bool Delete(long renovationAppointmentId)
        {
            var renovationAppointments = (List<RenovationAppointment>)GetAll();

            foreach (RenovationAppointment ra in renovationAppointments)
            {
                if (ra.Id == renovationAppointmentId)
                {
                    renovationAppointments.Remove(ra);
                    WriteLinesToFile(_path, RenovationAppointmentsToCSVFormat((List<RenovationAppointment>)renovationAppointments));
                    return true;
                }
            }
            return false;
        }

        private string RenovationAppointmentToCSVFormat(RenovationAppointment renovationAppointment)
        {
            string renovationAppointmentInCSVFormat = "";

            if(renovationAppointment.RenovationType.Id == 1)
            {
                renovationAppointmentInCSVFormat = string.Join(_delimiter,
                   renovationAppointment.Id,
                   renovationAppointment.Room.Id,
                   renovationAppointment.StartDate.ToString("dd.MM.yyyy."),
                   renovationAppointment.EndDate.ToString("dd.MM.yyyy."),
                   renovationAppointment.Description,
                   renovationAppointment.RenovationType.Id);
            }
            else if(renovationAppointment.RenovationType.Id == 2 || renovationAppointment.RenovationType.Id == 3)
            {
                renovationAppointmentInCSVFormat = string.Join(_delimiter,
                   renovationAppointment.Id,
                   renovationAppointment.Room.Id,
                   renovationAppointment.FirstRoom.Id,
                   renovationAppointment.SecondRoom.Id,
                   renovationAppointment.StartDate.ToString("dd.MM.yyyy."),
                   renovationAppointment.EndDate.ToString("dd.MM.yyyy."),
                   renovationAppointment.Description,
                   renovationAppointment.RenovationType.Id);
            }

            return renovationAppointmentInCSVFormat;
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

        private void WriteLinesToFile(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private RenovationAppointment CSVFormatToRenovationAppointment(string renovationAppointmentCSVFormat)
        {
            var tokens = renovationAppointmentCSVFormat.Split(_delimiter.ToCharArray());

            var timeFormat = "dd.MM.yyyy.";
            DateTime startTime;
            DateTime endTime;

           
            if(tokens.Length == 6)
            {
                DateTime.TryParseExact(tokens[2], timeFormat, CultureInfo.InvariantCulture
                                                  , DateTimeStyles.None
                                                  , out startTime);
                DateTime.TryParseExact(tokens[3], timeFormat, CultureInfo.InvariantCulture
                                                    , DateTimeStyles.None
                                                    , out endTime);
                return new RenovationAppointment(
                    long.Parse(tokens[0]),
                    long.Parse(tokens[1]),
                    startTime,
                    endTime,
                    tokens[4],
                    long.Parse(tokens[5]));
            }
            else
            {
                DateTime.TryParseExact(tokens[4], timeFormat, CultureInfo.InvariantCulture
                                                  , DateTimeStyles.None
                                                  , out startTime);
                DateTime.TryParseExact(tokens[5], timeFormat, CultureInfo.InvariantCulture
                                                    , DateTimeStyles.None
                                                    , out endTime);
                return new RenovationAppointment(
                    long.Parse(tokens[0]),
                    long.Parse(tokens[1]),
                    long.Parse(tokens[2]),
                    long.Parse(tokens[3]),
                    startTime,
                    endTime,
                    tokens[6],
                    long.Parse(tokens[7]));
            }
        }

        private List<string> RenovationAppointmentsToCSVFormat(List<RenovationAppointment> renovationAppointments)
        {
            List<string> lines = new List<string>();

            foreach (RenovationAppointment renovationAppointment in renovationAppointments)
            {
                lines.Add(RenovationAppointmentToCSVFormat(renovationAppointment));
            }
            return lines;
        }
    }
}
