using NHibernate;
using NHibernate.Criterion;
using SaveMW.Models.Filters;

namespace SaveMW.Models.Repositories
{
    [Repository]
    class TagRepository : Repository<Tag, TagFilter>
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
                    crit.Add(Restrictions.Like("Name", filter.Name, MatchMode.Anywhere));
                }                
            }
        }
    }
}
