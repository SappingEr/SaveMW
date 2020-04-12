using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class NoteMap : ClassMap<Note>
    {
        public NoteMap()
        {
            Id(n => n.Id);
            Map(n => n.Name).Length(150);
            Map(n => n.Text).Length(int.MaxValue);
            Map(n => n.CreationDate);
            References(n => n.Author);
            References(n => n.File);
            HasMany(n => n.Tags).Cascade.All();
        }
    }
}
