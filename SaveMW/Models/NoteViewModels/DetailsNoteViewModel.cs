using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaveMW.Models.NoteViewModels
{
    public class DetailsNoteViewModel : NoteViewModel
    {
        public int AuthorId { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Автор")]
        public string AuthorFIO { get; set; }

        [Display(Name = "Теги")]
        public IEnumerable<Tag> Tags { get; set; }

        [Display(Name = "Документы")]
        public IEnumerable<FSFile> Files { get; set; }
    }
}