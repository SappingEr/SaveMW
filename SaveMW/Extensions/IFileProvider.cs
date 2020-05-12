using SaveMW.Models;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace SaveMW.Extensions
{
    public interface IFileProvider<T>
        where T : class
    {
        FileProviderOp Provider { get; }

        IList<T> Save(HttpPostedFileBase[] files);

        IList<T> AjaxSave(HttpFileCollectionBase files);

        FileStream Load(int fileId);

        bool Delete(int fileId);
    }
}
