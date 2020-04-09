using NHibernate;
using NHibernate.Criterion;
using SaveMW.Models.Filters;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;

namespace SaveMW.Models.Repositories
{
    public class Repository<T, FT>
    where T : class
    where FT : BaseFilter
    {
        protected ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public virtual T Load(int? id)
        {
            return session.Get<T>(id);
        }       

        public virtual void Save(T entity)
        {
            session.Save(entity);
        }

        public virtual void SaveOrUpdate(T entity)
        {
            session.SaveOrUpdate(entity);
        }

        public virtual IList<T> FindAll()
        {
            var crit = session.CreateCriteria<T>();
            return crit.List<T>();
        }

        public void Flush()
        {
            session.Flush();
        }

        public virtual void SetupFilter(FT filter, ICriteria crit)
        {
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchString))
                {
                    SetupFastSearchFilter(crit, filter.SearchString);
                }
            }
        }

        public virtual void SetupFastSearchFilter(ICriteria crit, string searchStr)
        {
            ICriterion criterion = null;
            foreach (var prop in typeof(T).GetProperties())
            {
                var attr = prop.GetCustomAttribute<FastSearchAttribute>();
                if (attr == null)
                {
                    continue;
                }
                var likeExpresion = Restrictions.Like(prop.Name, searchStr, MatchMode.Anywhere);
                criterion = criterion == null ? likeExpresion : Restrictions.Or(criterion, likeExpresion);
            }
            if (criterion != null)
            {
                crit.Add(criterion);
            }
        }

        public virtual void SetupFetchOptions(ICriteria crit, FetchOptions options)
        {
            if (options != null)
            {
                if (options.Start > 0)
                {
                    crit.SetFirstResult(options.Start);
                }
                if (options.Count > 0)
                {
                    crit.SetMaxResults(options.Count);
                }
                if (!string.IsNullOrEmpty(options.SortExpression))
                {
                    crit.AddOrder(options.SortDirection == SortDirection.Ascending ?
                        Order.Asc(options.SortExpression) :
                        Order.Desc(options.SortExpression));
                }
            }
        }
    }

}
