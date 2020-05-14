using SaveMW.Extensions;
using SaveMW.Models;
using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveMW.Controllers
{
    public class DBFileController : Controller
    {
        private IFileProvider<DBFile> fileProvider;
        private DBFileRepository dBFileRepository;
        public DBFileController(DBFileRepository dBFileRepository, IFileProvider<DBFile> fileProvider)
        {
            this.fileProvider = fileProvider;
            this.dBFileRepository = dBFileRepository;
        }
        
        public ActionResult GetImage(int id)
        {
            DBFile file = dBFileRepository.Load(id);
            if (file != null && file.Extention == ".jpg")
            {                
                FileStream stream = fileProvider.Load(file.Id);
                return File(stream, file.Extention);
            }
            return new EmptyResult();
        }
    }
}