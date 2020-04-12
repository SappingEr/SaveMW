using Microsoft.AspNet.Identity.Owin;
using SaveMW.Models;
using SaveMW.Models.AccountViewModels;
using SaveMW.Models.Repositories;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController(UserRepository userRepository)
            : base(userRepository)
        {
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login() => View();

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var result = await SignInManager.PasswordSignInAsync(loginModel.Login,
                loginModel.Password, loginModel.RememberMe, false);

                switch (result)
                {
                    case SignInStatus.Success:
                        UserStatus status = UserManager.FindByNameAsync(loginModel.Login).Result.UserStatus;
                        if (status == UserStatus.Blocked)
                        {
                            ModelState.AddModelError("", "Аккаунт заблокирован администратором");
                            SignInManager.SignOut();
                            return View(loginModel);
                        }
                        return RedirectToAction("Index", "Home");

                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "Неверный логин или пароль");
                        break;
                }
            }
            return View(loginModel);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RegisterViewModel registerModel = new RegisterViewModel();
                return View(registerModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid && !User.Identity.IsAuthenticated)
            {
                var user = new User { UserName = registerModel.UserName, FIO = registerModel.FIO, UserStatus = UserStatus.Active };
                var result = await UserManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Логин занят, введите данные заново");
                    return View(registerModel);
                }
            }
            return View(registerModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public ActionResult ChangePass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = userRepository.Load(id);
            if (user != null)
            {
                ChangePassViewModel passModel = new ChangePassViewModel { Id = user.Id };
                return View(passModel);
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePass(ChangePassViewModel passModel)
        {
            if (ModelState.IsValid)
            {
                if (passModel.NewPassword == passModel.ConfirmNewPassword)
                {
                    User user = userRepository.Load(passModel.Id);
                    if (user != null)
                    {
                        var result = await UserManager.ChangePasswordAsync(user.Id, passModel.Password, passModel.NewPassword);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Info", new { id = passModel.Id });
                        }
                        ModelState.AddModelError("", "Ошибка смены пароля!");
                        return View(passModel);
                    }
                    return HttpNotFound("Пользователь не найден");
                }
                ModelState.AddModelError("", "Новые пароли не совпадают!");
                return View(passModel);
            }
            return View(passModel);
        }
    }
}