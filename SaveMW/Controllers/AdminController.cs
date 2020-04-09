using SaveMW.Models;
using SaveMW.Models.AdminViewModels;
using SaveMW.Models.Filters;
using SaveMW.Models.Repositories;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(UserRepository userRepository)
            : base(userRepository)
        {
        }

        [HttpGet]
        public ActionResult UserList(int? page, UserFilter filter, FetchOptions options)
        {               
            int count = 2;
            int start = ((page ?? 1) - 1) * count;
            options.Start = start;
            options.Count = count;
            int userCount = userRepository.FindAll().Count;
            var users = userRepository.Find(filter, options);
            Paging paging = new Paging { PageNumber = page ?? 1, PageSize = count, TotalItems = userCount };
            AdminIndexViewModel indexModel = new AdminIndexViewModel { Users = users, Paging = paging };
            return View(indexModel);
        }

        //[HttpPost, ValidateAntiForgeryToken]
        //public ActionResult UserList(UserFilter filter)
        //{

        //}
    }
}