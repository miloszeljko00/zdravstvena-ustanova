using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface ISpecialtyService : IService<Specialty>
{
    List<Doctor> GetDoctorsBySpecialty(Specialty specialty, IEnumerable<Doctor> doctors);
}