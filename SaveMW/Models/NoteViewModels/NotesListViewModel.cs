using System.Collections.Generic;

namespace SaveMW.Models.NoteViewModels
{
    public class NotesListViewModel
    {
        public int Id { get; set; }

        public IEnumerable<Note> Notes { get; set; }       
    }
}