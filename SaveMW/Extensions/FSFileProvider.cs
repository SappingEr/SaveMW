using SaveMW.Models;
using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SaveMW.Extensions
{
    public class FSFileProvider : IFileProvider
    {
        private FSFileRepository fSFileRepository;

        public FSFileProvider(FSFileRepository fSFileRepository)
        {
            this.fSFileRepository = fSFileRepository;
        }

        public FileProviderOp Provider { get; } = FileProviderOp.FileSys;

        public void Load(int fileId)
        {
           

            throw new NotImplementedException();
        }

        public IList<FSFile> Save(HttpPostedFileBase[] files)
        {
            int k = 0;
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
                    file.SaveAs(HttpContext.Current.Server.MapPath(@"~\App_Data\UploadFiles\") + fSFile.Id + k);                    
                    savedFiles.Add(fSFile);
                    k++;                   
                };
            }
            return savedFiles;
        }
    }
}