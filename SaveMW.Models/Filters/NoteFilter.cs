using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models.Filters
{
    public class NoteFilter: BaseFilter
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public DateRange CreationDate { get; set; }
    }
}
