using System.IO;

namespace AhkModule.AhkDotNet
{
    internal sealed class FtpUploadElement
    {
        internal FtpUploadElement(string local, bool isDirectory)
        {
            fsPath = local;
            isDir = isDirectory;
            Name = Path.GetFileName(local);

            if (!isDirectory)
                bytesSize = (int)new FileInfo(local).Length;
        }

        public string Type
        {
            get { return IsDirectory ? "directory" : "file"; }
        }

        public int Size
        {
            get { return bytesSize; }
        }

        public string LocalPath
        {
            get { return fsPath; }
        }

        public string Name
        {
            get;
            set;
        }

        public bool IsItemChecked
        {
            get;
            set;
        }

        internal bool IsDirectory
        {
            get { return isDir; }
        }

        private readonly bool isDir;
        private readonly string fsPath;
        private readonly int bytesSize;
    }
}
