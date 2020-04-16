using FluentNHibernate.Mapping;

namespace SaveMW.Models.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.UserName).Length(30);
            Map(u=>u.Password).Column("PasswordHash");
            Map(u => u.FIO).Length(150);
            Map(u => u.UserStatus);
            References(u => u.Avatar).Cascade.All();
            HasMany(u => u.Notes).Cascade.All();
        }

    }
}
