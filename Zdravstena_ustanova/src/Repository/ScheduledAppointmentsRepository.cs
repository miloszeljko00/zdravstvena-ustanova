using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Repository
{
    public class ScheduledAppointmentsRepository
    {
        public bool Save(List<ScheduledAppointment> scheduledAppointments)
        {

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("..\\..\\..\\data\\scheduledAppointments.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, scheduledAppointments);
            stream.Close();

            return true;
        }

        public List<ScheduledAppointment> Read()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("..\\..\\..\\data\\scheduledAppointments.txt", FileMode.Open, FileAccess.Read);

            List<ScheduledAppointment> scheduledAppointments = (List<ScheduledAppointment>)formatter.Deserialize(stream);
            return scheduledAppointments;
        }
    }
}