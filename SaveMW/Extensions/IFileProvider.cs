using SaveMW.Models;
using System.Collections.Generic;
using System.Web;

namespace SaveMW.Extensions
{
    public interface IFileProvider
    {
        FileProviderOp Provider { get; }

        IList<FSFile> Save(HttpPostedFileBase[] files);

        void Load(int fileId);
    }
}
