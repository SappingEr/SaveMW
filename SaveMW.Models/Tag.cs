using System.Collections.Generic;

namespace SaveMW.Models
{
    public class Tag
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Note> Notes { get; set; }
    }
}
