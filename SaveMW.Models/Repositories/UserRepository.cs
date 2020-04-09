using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Criterion;
using SaveMW.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SaveMW.Models.Repositories
{
    [Repository]
    public class UserRepository : Repository<User, UserFilter>
    {
        public UserRepository(ISession session) :
            base(session)
        {
        }

        public override void SetupFilter(UserFilter filter, ICriteria crit)
        {
            base.SetupFilter(filter, crit);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.UserName))
                {
                    crit.Add(Restrictions.Like("UserName", filter.UserName));
                }

                if (!string.IsNullOrEmpty(filter.FIO))
                {
                    crit.Add(Restrictions.Like("FIO", filter.FIO, MatchMode.Anywhere));
                }                
            }
        }

        public IList<User> Find(UserFilter filter, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<User>();           
            SetupFetchOptions(crit, options);
            SetupFilter(filter, crit);
            return crit.List<User>();
        }

        public int Count(UserFilter filter)
        {
            var crit = session.CreateCriteria<User>();
            SetupFilter(filter, crit);
            crit.SetProjection(Projections.Count("Id"));
            return Convert.ToInt32(crit.UniqueResult());
        }

        public User GetCurrentUser(IPrincipal user = null)
        {
            user = user ?? HttpContext.Current.User;
            if (user == null || user.Identity == null)
            {
                return null;
            }
            var currentUserId = user.Identity.GetUserId();
            int userId;
            if (string.IsNullOrEmpty(currentUserId) || !int.TryParse(currentUserId, out userId))
            {
                return null;
            }
            return Load(userId);
        }
    }
}
