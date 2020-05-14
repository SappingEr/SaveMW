using SaveMW.Extensions;
using SaveMW.Models;
using SaveMW.Models.UserViewModels;
using SaveMW.Models.Repositories;
using System.Net;
using System.Web.Mvc;
using System.Web;
using System.Linq;

namespace SaveMW.Controllers
{
    public class UserController : BaseController
    {
        private IFileProvider<DBFile> fileProvider;

        public UserController(UserRepository userRepository, IFileProvider<DBFile> fileProvider)
            : base(userRepository)
        {
            this.fileProvider = fileProvider;
        }

        [HttpGet]
        public ActionResult Info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.Load(id);
            if (user != null)
            {
                InfoViewModel infoModel = new InfoViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FIO = user.FIO,
                    NotesCount = user.Notes.Count
                };                
                return View(infoModel);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Avatar(int id)
        {
            User user = userRepository.Load(id);
            if (user != null)
            {
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = userRepository.Load(id);
            if (user != null)
            {
                EditViewModel editModel = new EditViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FIO = user.FIO
                };
                return View(editModel);
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                User user = userRepository.Load(editModel.Id);
                if (user != null)
                {
                    user.UserName = editModel.UserName;
                    user.FIO = editModel.FIO;
                    userRepository.Save(user);
                    userRepository.Flush();
                    return RedirectToAction("Info", new { id = editModel.Id });
                }
                return HttpNotFound("Пользователь не найден");
            }
            return View(editModel);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AjaxAvatarUpload(int id)
        {
            HttpFileCollectionBase uploadFiles = Request.Files;
            User user = userRepository.Load(id);
            if (user != null)
            {
                DBFile avatar = fileProvider.AjaxSave(uploadFiles).FirstOrDefault();
                user.Avatar = avatar;
                userRepository.Save(user);
                userRepository.Flush();
                return Json(new { success = true });
            }
            return Json(new { success = false, responseText = "Ошибка! Не удалось загрузить файл." });
        }
    }
}