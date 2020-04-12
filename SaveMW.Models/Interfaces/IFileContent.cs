using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models.Interfaces
{
    interface IFileContent
    {
        int Id { get; set; }

        string Name { get; set; }

        string FilePath { get; set; }
    }
}
