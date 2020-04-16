using NHibernate;
using SaveMW.Models.Filters;


namespace SaveMW.Models.Repositories
{
    [Repository]
    public class DBFileRepository : Repository<DBFile, DBFileFilter>
    {
        public DBFileRepository(ISession session) : base(session)
        {
        }
    }
}
