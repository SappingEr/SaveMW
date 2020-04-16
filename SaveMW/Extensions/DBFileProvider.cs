using SaveMW.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace SaveMW.Extensions
{
    public class DBFileProvider : IFileProvider
    {
        public FileProviderOp Provider => FileProviderOp.Database;

        public void Load(int fileId)
        {
            throw new NotImplementedException();
        }

        public IList<FSFile> Save(HttpPostedFileBase[] files)
        {
            throw new NotImplementedException();
        }
    }
}