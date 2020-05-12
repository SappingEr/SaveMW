using SaveMW.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SaveMW.Extensions
{
    public class DBFileProvider : IFileProvider<FSFile>
    {
        public FileProviderOp Provider => FileProviderOp.Database;

        public IList<FSFile> AjaxSave(HttpFileCollectionBase files)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int fileId)
        {
            throw new NotImplementedException();
        }

        public FileStream Load(int fileId)
        {
            throw new NotImplementedException();
        }

        public IList<FSFile> Save(HttpPostedFileBase[] files)
        {
            throw new NotImplementedException();
        }
    }
}