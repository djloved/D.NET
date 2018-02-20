using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace D.Net.IO
{
    public class AutoDeleteFileStream : FileStream, IDisposable
    {
        public string FilePath { get; set; }
        public AutoDeleteFileStream(string path, FileMode mode, FileAccess access)
        : base(path, mode, access)
        {
            FilePath = path;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (FilePath != null)
                FilePath.FileDelete();
        }
    }
}
