using Microsoft.AspNet.Identity.Owin;
using SaveMW.Auth;
using SaveMW.Models;
using SaveMW.Models.Repositories;
using System.Net;
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
    }
}