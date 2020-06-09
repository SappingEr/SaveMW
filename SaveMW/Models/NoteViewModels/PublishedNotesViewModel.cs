using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveMW.Models.NoteViewModels
{
    public class PublishedNotesViewModel
    {
        public IEnumerable<Note> Notes { get; set; }        
    }
}