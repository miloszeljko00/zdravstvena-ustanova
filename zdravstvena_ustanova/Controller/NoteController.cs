using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace zdravstvena_ustanova.Controller
{
   public class NoteController
   {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        public IEnumerable<Note> GetAll()
        {
            return _noteService.GetAll();
        }
        public Note GetById(long Id)
        {
            return _noteService.GetById(Id);
        }
        public Note Create(Note note)
        {
            return _noteService.Create(note);
        }
        public void Update(Note note)
        {
            _noteService.Update(note);
        }
        public void Delete(long noteId)
        {
            _noteService.Delete(noteId);
        }
    }
}