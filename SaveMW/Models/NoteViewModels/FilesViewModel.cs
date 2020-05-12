using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaveMW.Models.NoteViewModels
{
    public class FilesViewModel
    {
        [Display(Name = "Документы")]
        public IEnumerable<FSFile> Files { get; set; }
    }
}