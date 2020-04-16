using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models.Mappings
{
   public class DBFileMap: SubclassMap<DBFile>
    {
        public DBFileMap()
        {
            Map(u => u.Content).Length(int.MaxValue);
        }
    }
}
