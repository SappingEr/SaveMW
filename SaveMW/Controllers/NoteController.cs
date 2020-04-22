using SaveMW.Extensions;
using SaveMW.Models;
using SaveMW.Models.Filters;
using SaveMW.Models.NoteViewModels;
using SaveMW.Models.Repositories;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class NoteController : BaseController
    {
        private IFileProvider fileProvider;
        private TagSerializer tagSerializer;
        private NoteRepository noteRepository;

        public NoteController(UserRepository userRepository, NoteRepository noteRepository,
            IFileProvider fileProvider, TagSerializer tagSerializer) :
            base(userRepository)
        {
            this.fileProvider = fileProvider;
            this.tagSerializer = tagSerializer;
            this.noteRepository = noteRepository;
        }

        [HttpGet]
        public ActionResult Notes(int id, int? page, NoteFilter filter, FetchOptions options)
        {
            User user = userRepository.Load(id);
            if (user != null)
            {
                int count = 4;
                options.Start = ((page ?? 1) - 1) * count;
                options.Count = count;
                int noteCount = noteRepository.Count(user, filter);
                var notes = noteRepository.UserNotes(user, filter, options);
                Paging paging = new Paging { PageNumber = page ?? 1, PageSize = count, TotalItems = noteCount };
                NotesPagingListViewModel notesModel = new NotesPagingListViewModel { Id = id, Notes = notes, Paging = paging, FetchOptions = options };
                return View(notesModel);
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpGet]
        public ActionResult Search(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.Load(id);
            if (user != null)
            {
                return View();
            }
            return HttpNotFound("Пользователь не найден");
        }

        //[HttpPost, ValidateAntiForgeryToken]
        //public ActionResult Search(int? id)
        //{

        //}        

        [HttpGet]
        public ActionResult NewNote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.Load(id);
            if (user != null)
            {
                return View();
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewNote(HttpPostedFileBase[] files, NoteViewModel noteModel)
        {
            if (ModelState.IsValid)
            {

                User user = userRepository.Load(noteModel.Id);
                if (user != null)
                {
                    Note note = new Note
                    {
                        Name = noteModel.Name,
                        Text = noteModel.Text,
                        CreationDate = DateTime.Now
                    };
                    if (files != null && files.Length <= 3)
                    {
                        note.Files = fileProvider.Save(files);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Превышено допустимое количество файлов");
                        return View(noteModel);
                    }
                    if (noteModel.Tags != null)
                    {
                        note.Tags = tagSerializer.GetTags(noteModel.Tags);
                    }
                    user.Notes.Add(note);
                    userRepository.Save(user);
                    userRepository.Flush();
                    return RedirectToAction("Notes", "Note", new { id = user.Id });
                }
                return HttpNotFound("Пользователь не найден");
            }
            return View();
        }
    }
}