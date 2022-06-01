using System;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
   public class NoteController
   {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        public IEnumerable<Note> GetAll()
        {
            return _noteService.GetAll();
        }
        public Note GetById(long Id)
        {
            return _noteService.Get(Id);
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