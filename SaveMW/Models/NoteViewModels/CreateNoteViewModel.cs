using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaveMW.Models.NoteViewModels
{
    public class CreateNoteViewModel : NoteViewModel
    {
        [Display(Name = "Теги")]
        public string Tags { get; set; }        
    }
}