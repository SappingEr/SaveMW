using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(t => t.Id);
            Map(t => t.Name).Length(50);
            HasManyToMany(t => t.Notes);
        }
    }
}
