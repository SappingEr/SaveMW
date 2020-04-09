using Microsoft.AspNet.Identity;
using NHibernate;
using SaveMW.Models;
using System;
using System.Threading.Tasks;

namespace SaveMW.Auth
{
    public class IdentityStore : IUserStore<User, int>,
        IUserPasswordStore<User, int>,
        IUserLockoutStore<User, int>,
        IUserTwoFactorStore<User, int>
    {
        private readonly ISession session;

        public IdentityStore(ISession session)
        {
            this.session = session;
        }

        public Task CreateAsync(User user)
        {
            session.Save(user);
            session.Flush();
            return Task.FromResult(0);
        }

        public Task DeleteAsync(User user)
        {
            session.Delete(user);
            session.Flush();
            return Task.FromResult(0);
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.Run(() => session.Get<User>(userId));
        }

        public Task<User> FindByNameAsync(string username)
        {
            return Task.Run(() =>
            {
                return session.QueryOver<User>()
                    .Where(u => u.UserName == username)
                    .SingleOrDefault();
            });
        }

        public Task UpdateAsync(User user)
        {
            session.Update(user);
            session.Flush();
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Run(() => user.Password = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(true);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(DateTimeOffset.MaxValue);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public void Dispose()
        {
        }
    }
}