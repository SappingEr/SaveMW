using SaveMW.Models;
using SaveMW.Models.AdminViewModels;
using SaveMW.Models.Filters;
using SaveMW.Models.Repositories;
using System.Net;
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
            int count = 20;           
            options.Start = ((page ?? 1) - 1) * count;
            options.Count = count;
            int userCount = userRepository.Count(filter);
            var users = userRepository.Find(filter, options);
            Paging paging = new Paging { PageNumber = page ?? 1, PageSize = count, TotalItems = userCount };
            AdminUserListViewModel indexModel = new AdminUserListViewModel { Users = users, Paging = paging, FetchOptions = options };
            return View(indexModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UserStatus(int id)
        {
            User user = userRepository.Load(id);
            if (user != null)
            {
                UserStatus status = user.UserStatus;

                string text = null;

                switch (status)
                {
                    case Models.UserStatus.Active:
                        user.UserStatus = Models.UserStatus.Blocked;
                        text = $"Пользователь {user.UserName} - заблокирован!";
                        break;
                    case Models.UserStatus.Blocked:
                        user.UserStatus = Models.UserStatus.Active;
                        text = $"Пользователь {user.UserName} - разблокирован!";
                        break;
                }
                userRepository.Save(user);
                userRepository.Flush();
                return Json(new { success = true, responseText = text });
            }
            return Json(new { success = false, responseText = "Ошибка! Пользователь не обнаружен." });
        }

        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.Load(id);
            if (user != null)
            {
                DeleteUserViewModel deleteModel = new DeleteUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FIO = user.FIO
                };
                return View(deleteModel);
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteUser([Bind(Include ="Id")] DeleteUserViewModel deleteModel)
        {
            User user = userRepository.Load(deleteModel.Id);
            if (user != null)
            {
                userRepository.Delete(user);
                userRepository.Flush();
                return RedirectToAction("UserList");
            }
            return HttpNotFound("Пользователь не найден");
        }
    }
}