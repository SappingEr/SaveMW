using SaveMW.Extensions;
using SaveMW.Models;
using SaveMW.Models.Filters;
using SaveMW.Models.NoteViewModels;
using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    [Authorize]
    public class NoteController : BaseController
    {
        private IFileProvider<FSFile> fileProvider;
        private TagSerializer tagSerializer;
        private NoteRepository noteRepository;
        private TagRepository tagRepository;
        public NoteController(UserRepository userRepository, NoteRepository noteRepository,
            IFileProvider<FSFile> fileProvider, TagRepository tagRepository, TagSerializer tagSerializer) :
            base(userRepository)
        {
            this.fileProvider = fileProvider;
            this.tagSerializer = tagSerializer;
            this.noteRepository = noteRepository;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public ActionResult Notes(int id, int? page, NoteFilter filter, FetchOptions options)
        {
            User user = userRepository.Load(id);
            if (user != null)
            {
                User currentUser = userRepository.GetCurrentUser();
                if (user.Id != currentUser.Id)
                {
                    filter.Show = true;
                }
                int count = 6;
                options.Start = ((page ?? 1) - 1) * count;
                options.Count = count;
                int noteCount = noteRepository.Count(user, filter);
                var notes = noteRepository.UserNotes(user, filter, options);
                Paging paging = new Paging { PageNumber = page ?? 1, PageSize = count, TotalItems = noteCount };
                NotesPagingListViewModel notesModel = new NotesPagingListViewModel
                {
                    Id = id,
                    UserName = user.UserName,
                    FIO = user.FIO,
                    Notes = notes,
                    Paging = paging,
                    FetchOptions = options
                };
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
                if (!userRepository.CheckCurrentUser(user.Id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(new NotesListViewModel { Id = user.Id });
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Search(int id, NoteFilter filter)
        {
            if (ModelState.IsValid)
            {
                User user = userRepository.Load(id);
                if (user != null)
                {
                    NotesListViewModel notesModel = new NotesListViewModel();
                    notesModel.Id = user.Id;
                    notesModel.Notes = noteRepository.UserNotes(user, filter);
                    return View(notesModel);
                }
                return HttpNotFound("Пользователь не найден");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Published()
        {
            int countUploadNotes = 3;
            int notesCount = noteRepository.PublishedNotes().Count();
            return View(new PublishedViewModel { CountUploadNotes = countUploadNotes, NotesCount = notesCount });
        }

        [HttpGet]
        public ActionResult GetPublishedNotes(int count, int? page)
        {
            FetchOptions options = new FetchOptions();
            options.Start = ((page ?? 1) - 1) * count;
            options.Count = count;
            var notes = noteRepository.PublishedNotes(options);
            PublishedNotesViewModel publishedModel = new PublishedNotesViewModel { Notes = notes };
            return PartialView(publishedModel);
        }

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
                if (!userRepository.CheckCurrentUser(user.Id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(new CreateNoteViewModel { Id = user.Id });
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewNote(HttpPostedFileBase[] files, CreateNoteViewModel noteModel)
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
                        Show = noteModel.Show,
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
                        List<Tag> deserializedTags = tagSerializer.DeserializeTags(noteModel.Tags);
                        note.Tags = tagRepository.TagsToAdd(deserializedTags);
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

        [HttpGet]
        public ActionResult EditNote(int? id, int? userId)
        {
            if (id == null && userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepository.Load(userId);
            if (user != null)
            {
                if (!userRepository.CheckCurrentUser(user.Id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Note note = user.Notes.Where(n => n.Id == id).FirstOrDefault();

                if (note != null && note.Author.Id == user.Id)
                {
                    CreateNoteViewModel noteModel = new CreateNoteViewModel
                    {
                        Id = note.Id,
                        Name = note.Name,
                        Text = note.Text,
                        CreationDate = note.CreationDate,
                        Show = note.Show
                    };
                    if (note.Tags.Count > 0)
                    {
                        noteModel.Tags = tagSerializer.SerializeTags(note.Tags.ToArray());
                    }
                    return View(noteModel);
                }
                HttpNotFound("Заметка не найдена");
            }
            return HttpNotFound("Пользователь не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditNote(CreateNoteViewModel noteModel)
        {
            if (ModelState.IsValid)
            {
                Note note = noteRepository.Load(noteModel.Id);
                if (note != null)
                {
                    note.Name = noteModel.Name;
                    note.Text = noteModel.Text;
                    note.Show = noteModel.Show;

                    if (noteModel.Tags != null)
                    {
                        List<Tag> deserializedTags = tagSerializer.DeserializeTags(noteModel.Tags);
                        note.Tags = tagRepository.TagsToAdd(deserializedTags).ToList();
                    }
                    noteRepository.Save(note);
                    noteRepository.Flush();
                    return RedirectToAction("Note", "Note", new { id = note.Id });
                }
                return HttpNotFound("Заметка не найдена.");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Files(int id)
        {
            Note note = noteRepository.Load(id);
            FilesViewModel filesModel = new FilesViewModel();
            if (note != null)
            {
                if (note.Files.Count > 0)
                {
                    filesModel.Files = note.Files;
                    return PartialView(filesModel);
                }
            }
            filesModel.Files = null;
            return PartialView(filesModel);
        }

        [HttpGet]
        public ActionResult Note(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteRepository.Load(id);

            if (note != null)
            {
                int userId = userRepository.GetCurrentUser().Id;
                DetailsNoteViewModel noteModel = new DetailsNoteViewModel
                {
                    Id = note.Id,
                    Name = note.Name,
                    AuthorId = note.Author.Id,
                    UserName = note.Author.UserName,
                    Text = note.Text,
                    Tags = note.Tags,
                    Files = note.Files,
                    CreationDate = note.CreationDate
                };
                if (note.Show == true && userId != note.Author.Id)
                {
                    noteModel.AuthorFIO = note.Author.FIO;
                }
                else if (note.Show == false && userId != note.Author.Id)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(noteModel);
            }
            return HttpNotFound("Заметка не найдена");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AjaxFileUpload(int id)
        {
            HttpFileCollectionBase uploadFiles = Request.Files;
            Note note = noteRepository.Load(id);
            if (note != null)
            {
                List<FSFile> files = fileProvider.AjaxSave(uploadFiles).ToList();
                foreach (var file in files)
                {
                    note.Files.Add(file);
                }
                noteRepository.Save(note);
                noteRepository.Flush();
                return Json(new { success = true });
            }
            return Json(new { success = false, responseText = "Ошибка! Не удалось загрузить файл." });
        }
    }
}