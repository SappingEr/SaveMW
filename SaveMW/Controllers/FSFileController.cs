using SaveMW.Extensions;
using SaveMW.Models;
using SaveMW.Models.Repositories;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class FSFileController : Controller
    {
        private IFileProvider<FSFile> fileProvider;
        private FSFileRepository fSFileRepository;
        

        public FSFileController(FSFileRepository fSFileRepository, IFileProvider<FSFile> fileProvider)
        {
            this.fileProvider = fileProvider;
            this.fSFileRepository = fSFileRepository;            
        }        

        [HttpGet]
        public ActionResult Download(int id)
        {
            FSFile file = fSFileRepository.Load(id);
            if (file != null)
            {
                FileStream stream = fileProvider.Load(file.Id);
                return File(stream, file.Extention, file.Name);
            }
            return HttpNotFound("Файл не найден");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AjaxDelete(int id)
        {
            FSFile file = fSFileRepository.Load(id);
            if (file != null)
            {
                bool deleteFile = fileProvider.Delete(file.Id);
                if (deleteFile)
                {
                    fSFileRepository.Delete(file);
                    fSFileRepository.Flush();
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false, responseText = "Ошибка! Не удалось удалить файл." });
        }
    }
}