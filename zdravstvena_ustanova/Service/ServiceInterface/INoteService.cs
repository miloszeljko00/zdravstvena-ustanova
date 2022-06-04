using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface INoteService : IService<Note>
{
    IEnumerable<Note> GetNotesByPatient(long patientId);
}