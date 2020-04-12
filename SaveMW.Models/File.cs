using SaveMW.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models
{
    public class File: IFileContent
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string FilePath { get; set; }
    }
}
