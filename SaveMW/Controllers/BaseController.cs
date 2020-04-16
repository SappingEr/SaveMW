using Microsoft.AspNet.Identity.Owin;
using SaveMW.Auth;
using SaveMW.Extensions;
using SaveMW.Models;
using SaveMW.Models.Repositories;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class BaseController : Controller
    {
        protected UserRepository userRepository;

        public BaseController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

       

        public SignInManager SignInManager => HttpContext.GetOwinContext().Get<SignInManager>();

        public UserManager UserManager => HttpContext.GetOwinContext().GetUserManager<UserManager>();

        //public RoleManager RoleManager => HttpContext.GetOwinContext().Get<RoleManager>();

        public User CurrentUser => userRepository.GetCurrentUser(User);

        //public IFileProvider GetFileProvider(FileProviderOp provider)
        //{
        //    var key = ConfigurationManager.AppSettings["FileProvider"];
        //    IFileProvider typeProvider = fileProviders.FirstOrDefault(p => p.Provider == provider);
        //    if (typeProvider != null)
        //    {
        //        return typeProvider;
        //    }
        //    return null;
        //}
    }
}