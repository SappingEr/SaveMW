using System.ComponentModel.DataAnnotations;

namespace SaveMW.Models.Filters
{
    public class NoteFilter : BaseFilter
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }
        
        public DateRange CreationDate { get; set; }

        public bool Show { get; set; }
    }
}
