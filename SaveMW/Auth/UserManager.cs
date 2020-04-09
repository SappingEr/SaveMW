using Microsoft.AspNet.Identity;
using SaveMW.Models;

namespace SaveMW.Auth
{
    public class UserManager : UserManager<User, int>
    {
        public UserManager(IUserStore<User, int> store)
            : base(store)
        {
            UserValidator = new UserValidator<User, int>(this);
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5
            };
        }
    }
}