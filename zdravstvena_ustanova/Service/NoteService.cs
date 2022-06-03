using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private IPatientRepository _patientRepository;

        public NoteService(INoteRepository noteRepository, IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            _noteRepository = noteRepository;
        }

        public IEnumerable<Note> GetAll()
        {
            var patients = _patientRepository.GetAll();
            var notes = _noteRepository.GetAll();
            BindPatientsWithNotes(patients, notes);
            return notes;
        }
        public IEnumerable<Note> GetNotesByPatient(long patientId)
        {
            var notes = GetAll();
            List<Note> patientsNotes = new List<Note>();
            foreach (Note n in notes)
            {
                if (patientId == n.Patient.Id)
                {
                    patientsNotes.Add(n);
                }
            }
            return patientsNotes;
        }

        public Note Get(long Id)
        {
            var patients = _patientRepository.GetAll();
            var note = _noteRepository.Get(Id);
            BindPatientsWithNote(patients, note);
            return note;
        }
        private void BindPatientsWithNote(IEnumerable<Patient> patients, Note note)
        {
            note.Patient = FindPatientById(patients, note.Patient.Id);

        }

        private void BindPatientsWithNotes(IEnumerable<Patient> patients, IEnumerable<Note> notes)
        {
            notes.ToList().ForEach(note =>
            {
                BindPatientsWithNote(patients, note);
            });
        }

        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }
        public Note Create(Note note)
        {
            return _noteRepository.Create(note);
        }
        public bool Update(Note note)
        {
            return _noteRepository.Update(note);
        }
        public bool Delete(long noteId)
        {
            return _noteRepository.Delete(noteId);
        }
    }
}