using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models
{
    public class Note
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Text { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual User Author { get; set; }

        public virtual File File { get; set; }

        public virtual IList<Tag> Tags { get; set; } = new List<Tag>();
    }
}
