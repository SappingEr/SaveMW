using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class NoteMap : ClassMap<Note>
    {
        public NoteMap()
        {
            Id(n => n.Id);
            Map(n => n.Name).Length(300);
            Map(n => n.Text).Length(10000);
            Map(n => n.CreationDate);
            References(n => n.Author);
            HasMany(n => n.Files).Cascade.All();
            HasMany(n => n.Tags).Cascade.All();
        }
    }
}
