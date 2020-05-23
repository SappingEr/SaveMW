using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class DBFileMap : SubclassMap<DBFile>
    {
        public DBFileMap()
        {
           Map(f => f.Content).Length(int.MaxValue);
        }
    }
}
