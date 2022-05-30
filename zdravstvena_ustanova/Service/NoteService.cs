using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using zdravstvena_ustanova.Repository;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Service
{
    public class NoteService
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

        public Note GetById(long Id)
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
        public void Update(Note note)
        {
            _noteRepository.Update(note);
        }
        public void Delete(long noteId)
        {
            _noteRepository.Delete(noteId);
        }
    }
}