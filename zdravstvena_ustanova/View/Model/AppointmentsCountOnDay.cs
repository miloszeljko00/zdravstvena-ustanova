using System;

namespace zdravstvena_ustanova.View.Model;

public class AppointmentsCountOnDay
{
    public DateTime Date { get; set; }
    public int NumberOfAppointments { get; set; }

    public AppointmentsCountOnDay(DateTime date, int numberOfAppointments)
    {
        Date = date;
        NumberOfAppointments = numberOfAppointments;
    }
}