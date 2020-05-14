using SaveMW.Models;
using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SaveMW.Extensions
{
    public class DBFileProvider : IFileProvider<DBFile>
    {
        private DBFileRepository dBFileRepository;
        public DBFileProvider(DBFileRepository dBFileRepository)
        {
            this.dBFileRepository = dBFileRepository;
        }

        public IList<DBFile> AjaxSave(HttpFileCollectionBase files)
        {
            IList<DBFile> savedFiles = new List<DBFile>();
            foreach (string file in files)
            {
                var upload = files[file];
                if (upload != null)
                {
                    string fileName = Path.GetFileName(upload.FileName);
                    DBFile dBFile = new DBFile
                    {
                        Name = fileName,
                        Extention = Path.GetExtension(fileName)                        
                    };
                    dBFile.Content = new byte[upload.ContentLength];
                    upload.InputStream.Read(dBFile.Content, 0, upload.ContentLength);
                    dBFileRepository.Save(dBFile);                    
                    savedFiles.Add(dBFile);                    
                }
            }
            return savedFiles;
        }

        public bool Delete(int fileId)
        {
            throw new NotImplementedException();
        }

        public FileStream Load(int fileId)
        {
            DBFile file = dBFileRepository.Load(fileId);
            if (file != null)
            {
                var content = file.Content;
                var path = Path.GetTempFileName();
                var fileName = Path.GetFileNameWithoutExtension(path);
                var extension = Path.GetExtension(file.Name);
                var fullPath = Path.Combine(Path.GetDirectoryName(path), $"{fileName}{extension}");
                using (var stream = new FileStream(fullPath, FileMode.CreateNew))
                {
                    stream.Write(content, 0, content.Length);
                }
                return new FileStream(fullPath, FileMode.Open);
            }
            throw new NotImplementedException();
        }

        public IList<DBFile> Save(HttpPostedFileBase[] files)
        {
            throw new NotImplementedException();
        }
        
    }
}