using System;
using System.Net;
using System.Text.RegularExpressions;

namespace AhkModule.AhkDotNet
{
    internal sealed class FtpFile : FtpElement
    {
        internal FtpFile(Match data, Uri directory)
            : base(data, directory)
        {
            var ext = System.IO.Path.GetExtension(Name);
            // todo: read registry info on this file type
            Description = "file";
        }

        public override System.Windows.Media.ImageSource Icon
        {
            get { return FtpElement.IconFile; }
        }
    }
}
