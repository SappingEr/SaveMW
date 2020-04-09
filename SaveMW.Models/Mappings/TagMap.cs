using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models.Mappings
{
   public class TagMap: ClassMap<Tag>
    {
        public TagMap()
        {
            Id(t => t.Id);
            Map(t => t.Name);
            References(t => t.Note);
        }
    }
}
