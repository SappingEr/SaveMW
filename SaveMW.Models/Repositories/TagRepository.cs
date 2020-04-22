using NHibernate;
using NHibernate.Criterion;
using SaveMW.Models.Filters;
using System.Collections.Generic;

namespace SaveMW.Models.Repositories
{
    [Repository]
    public class TagRepository : Repository<Tag, TagFilter>
    {
        public TagRepository(ISession session) :
           base(session)
        {
        }

        public override void SetupFilter(TagFilter filter, ICriteria crit)
        {
            base.SetupFilter(filter, crit);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    crit.Add(Restrictions.Eq("Name", filter.Name));
                }                
            }
        }

        public IList<Tag> SavedTags(string[] tags)
        {
            var crit = session.CreateCriteria<Tag>()
                .Add(Restrictions.InG("Name", tags));
            return crit.List<Tag>();
        }

        public Tag FindTag(User user, TagFilter filter)
        {
            var crit = session.CreateCriteria<Tag>()
                .CreateCriteria("User")
                .Add(Restrictions.Eq("User", user));
            SetupFilter(filter, crit);           
            return crit.UniqueResult<Tag>();
        }
    }
}
