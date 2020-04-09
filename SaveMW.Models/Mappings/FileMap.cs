using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class FileMap : ClassMap<File>
    {
        public FileMap()
        {
            Id(f => f.Id);
            Map(f => f.Name).Length(30);
            Map(f => f.Path).Length(200);
        }
    }
}
