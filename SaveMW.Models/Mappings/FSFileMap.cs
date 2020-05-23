using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class FSFileMap : SubclassMap<FSFile>
    {
        public FSFileMap()
        {
            References(f => f.Note);
            Map(f => f.Key).Length(5);           
        }
    }
}
