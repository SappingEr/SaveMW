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
                    crit.Add(Restrictions.Like("NoteName", filter.Name, MatchMode.Anywhere));
                }

                if (!string.IsNullOrEmpty(filter.Author))
                {
                    crit.Add(Restrictions.Like("Author", filter.Author, MatchMode.Anywhere));
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
    }
}
