using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class FileDetailsMap : ClassMap<FileDetails>
    {
        public FileDetailsMap()
        {
            Id(f => f.Id);
            Map(f => f.Name).Length(300);
            Map(f => f.Extention).Length(10);
        }
    }
}
