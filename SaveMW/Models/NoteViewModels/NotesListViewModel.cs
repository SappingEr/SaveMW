using System.Collections.Generic;

namespace SaveMW.Models.NoteViewModels
{
    public class NotesListViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FIO { get; set; }

        public IEnumerable<Note> Notes { get; set; }       
    }
}