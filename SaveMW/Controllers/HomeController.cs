using SaveMW.Models.Repositories;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserRepository userRepository): base(userRepository)
        {
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}