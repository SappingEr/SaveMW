using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models
{
    public class File
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Path { get; set; }
    }
}
