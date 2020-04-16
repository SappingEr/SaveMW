using NHibernate;
using SaveMW.Models.Filters;


namespace SaveMW.Models.Repositories
{
    [Repository]
    public class FSFileRepository : Repository<FSFile, FSFileFilter>
    {
        public FSFileRepository(ISession session) : base(session)
        {
        }
    }
}
