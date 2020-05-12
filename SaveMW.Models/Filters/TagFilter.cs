using System.ComponentModel.DataAnnotations;

namespace SaveMW.Models.Filters
{

    public class TagFilter : BaseFilter
    {
        [Display(Name = "Тег")]
        public string Name { get; set; }
    }
}
