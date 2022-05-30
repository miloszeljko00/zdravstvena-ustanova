using zdravstvena_ustanova.Exception;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using zdravstvena_ustanova.Repository.RepositoryInterface;

namespace zdravstvena_ustanova.Repository
{
    public class NoteRepository : INoteRepository
    {
        private const string NOT_FOUND_ERROR = "NOTE NOT FOUND: {0} = {1}";
        private readonly string _path;
        private readonly string _delimiter;
        private long _noteMaxId;

        public NoteRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _noteMaxId = GetMaxId(GetAll());
        }

        private long GetMaxId(IEnumerable<Note> notes)
        {
            return notes.Count() == 0 ? 0 : notes.Max(note => note.Id);
        }

        public IEnumerable<Note> GetAll()
        {
            return File.ReadAllLines(_path)
                .Select(CSVFormatToNote)
                .ToList();
        }

        public Note Get(long id)
        {
            try
            {
                return GetAll().SingleOrDefault(note => note.Id == id);
            }
            catch (ArgumentException)
            {
                throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
            }
        }

        public Note Create(Note note)
        {
            note.Id = ++_noteMaxId;
            AppendLineToFile(_path, NoteToCSVFormat(note));
            return note;
        }

        public bool Update(Note note)
        {
            var notes = GetAll();

            foreach (Note n in notes)
            {
                if(n.Id == note.Id)
                {
                    n.Name = note.Name;
                    n.Content = note.Content;
                    n.Time = note.Time;
                    WriteLinesToFile(_path, NotesToCSVFormat((List<Note>)notes));
                    return true;
                }
            }

            return false;
        }
        public bool Delete(long noteId)
        {
            var notes = (List<Note>)GetAll();

            foreach (Note n in notes)
            {
                if (n.Id == noteId)
                {
                   notes.Remove(n);
                   break;
                }
            }


            WriteLinesToFile(_path, NotesToCSVFormat((List<Note>)notes));
            return true;
        }

        private string NoteToCSVFormat(Note note)
        {
            return string.Join(_delimiter,
                note.Id,
                note.Patient.Id,
                note.Name,
                note.Content,
                note.Time
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

        private Note CSVFormatToNote(string noteCSVFormat)
        {
            var tokens = noteCSVFormat.Split(_delimiter.ToCharArray());
            
            return new Note(
               long.Parse(tokens[0]),
               long.Parse(tokens[1]),
               tokens[2],
               tokens[3],
               tokens[4]
                );
        }

        private List<string> NotesToCSVFormat(List<Note> notes)
        {
            List<string> lines = new List<string>();

            foreach (Note n in notes)
            {
                lines.Add(NoteToCSVFormat(n));
            }
            return lines;
        }
    }
}