using SaveMW.Models;
using SaveMW.Models.Filters;
using SaveMW.Models.NoteViewModels;
using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class TagController : BaseController
    {
        private TagRepository tagRepository;

        public TagController(UserRepository userRepository, TagRepository tagRepository) : base(userRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult TagSearch(int id, TagFilter tagFilter)
        {
            User user = userRepository.Load(id);
            if (user != null)
            {
                Tag tag = tagRepository.FindTag(tagFilter);
                if (tag != null)
                {
                    NotesListViewModel notesModel = new NotesListViewModel
                    {
                        Id = id,
                        Notes = tag.Notes.Where(t => t.Author == user)
                    };
                    return PartialView(notesModel);
                }
                return Json(new { success = false, responseText = "Поиск не дал результатов..." });
            }
            return Json(new { success = false, responseText = "Ошибка! Не найден пользователь." });
        }
    }
}