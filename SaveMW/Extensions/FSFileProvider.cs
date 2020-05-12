using SaveMW.Models;
using SaveMW.Models.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SaveMW.Extensions
{
    public class FSFileProvider : IFileProvider<FSFile>
    {
        private FSFileRepository fSFileRepository;

        public FSFileProvider(FSFileRepository fSFileRepository)
        {
            this.fSFileRepository = fSFileRepository;
        }

        public FileProviderOp Provider { get; } = FileProviderOp.FileSys;

        private int k = 0;

        private string directoryPath = HttpContext.Current.Server.MapPath(@"~\App_Data\UploadFiles\");

        public FileStream Load(int fileId)
        {
            FSFile file = fSFileRepository.Load(fileId);
            if (file != null)
            {
                string filepath = directoryPath + file.Id + file.Key;
                FileStream fs = new FileStream(filepath, FileMode.Open);               
                string fileType = file.Extention;
                return fs;
            }
            return null;
        }

        public IList<FSFile> Save(HttpPostedFileBase[] files)
        {            
            IList<FSFile> savedFiles = new List<FSFile>();
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    FSFile fSFile = new FSFile
                    {
                        Name = fileName,
                        Extention = Path.GetExtension(fileName),
                        Key = k
                    };
                    fSFileRepository.Save(fSFile);
                    file.SaveAs(directoryPath + fSFile.Id + k);
                    savedFiles.Add(fSFile);
                    k++;
                };
            }
            return savedFiles;
        }

        public bool Delete(int fileId)
        {
            FSFile file = fSFileRepository.Load(fileId);
            if (file != null)
            {
                string filePath = directoryPath + file.Id + file.Key;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }                
            }
            return false;
        }

        public IList<FSFile> AjaxSave(HttpFileCollectionBase files)
        {
            IList<FSFile> savedFiles = new List<FSFile>();
            foreach (string file in files)
            {
                var upload = files[file];
                if (upload != null)
                {                   
                    string fileName = Path.GetFileName(upload.FileName);
                    FSFile fSFile = new FSFile
                    {
                        Name = fileName,
                        Extention = Path.GetExtension(fileName),
                        Key = k
                    };
                    fSFileRepository.Save(fSFile);
                    upload.SaveAs(directoryPath + fSFile.Id + k);
                    savedFiles.Add(fSFile);
                    k++;                    
                }
            }
            return savedFiles;
        }
    }
}