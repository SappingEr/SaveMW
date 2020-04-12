using SaveMW.Models;
using SaveMW.Models.NoteViewModels;
using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class NoteController : BaseController
    {
        public NoteController(UserRepository userRepository) :
            base(userRepository)
        {
        }

        [HttpGet]
        public ActionResult Notes()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewNote() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewNote(HttpPostedFileBase[] files, NoteViewModel noteModel)
        {
            if (ModelState.IsValid)
            {
                var t = files;

                User user = userRepository.Load(noteModel.Id);
                if (user != null)
                {
                    Note note = new Note
                    {
                        Name = noteModel.Name,
                        Text = noteModel.Text,
                        CreationDate = DateTime.Now
                    };




                    userRepository.Save(user);
                    userRepository.Flush();
                    return RedirectToAction("Info");
                }
                return HttpNotFound("Пользователь не найден");
            }
            return View();
        }
    }
}