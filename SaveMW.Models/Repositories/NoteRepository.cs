using NHibernate;
using NHibernate.Criterion;
using SaveMW.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMW.Models.Repositories
{
    [Repository]
    public class NoteRepository : Repository<Note, NoteFilter>
    {
        public NoteRepository(ISession session) :
           base(session)
        {
        }

        public override void SetupFilter(NoteFilter filter, ICriteria crit)
        {
            base.SetupFilter(filter, crit);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    crit.Add(Restrictions.Like("Name", filter.Name, MatchMode.Anywhere));
                }               

                if (filter.CreationDate != null)
                {
                    if (filter.CreationDate.From.HasValue)
                    {
                        crit.Add(Restrictions.Ge("CreationDate", filter.CreationDate.From.Value));
                    }

                    if (filter.CreationDate.To.HasValue)
                    {
                        crit.Add(Restrictions.Le("CreationDate", filter.CreationDate.To.Value));
                    }
                }

            }
        }

        public IEnumerable<Note> UserNotes(User user, NoteFilter filter, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Note>()                
                .Add(Restrictions.Eq("Author", user));
            SetupFilter(filter, crit);
            SetupFetchOptions(crit, options);
            return crit.List<Note>();
        }         

        public int Count(User user, NoteFilter filter)
        {
            var crit = session.CreateCriteria<Note>()               
               .Add(Restrictions.Eq("Author", user));
            SetupFilter(filter, crit);
            crit.SetProjection(Projections.Count("Id"));
            return Convert.ToInt32(crit.UniqueResult());
        }
    }
}
