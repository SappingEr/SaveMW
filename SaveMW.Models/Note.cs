using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;

namespace SaveMW.Models
{
    public class Note
    {
        public virtual int Id { get; set; }

        [FastSearch]
        public virtual string Name { get; set; }

        public virtual string Text { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual User Author { get; set; }

        public virtual IList<FSFile> Files { get; set; }

        public virtual IList<Tag> Tags { get; set; }
    }
}
