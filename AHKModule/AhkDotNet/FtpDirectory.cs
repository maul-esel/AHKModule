using System;
using System.Text.RegularExpressions;

namespace AhkModule.AhkDotNet
{
    internal sealed class FtpDirectory : FtpElement
    {
        internal FtpDirectory(Match data, Uri directory)
            : base(data, directory)
        {
            Description = "directory";
        }

        public override System.Windows.Media.ImageSource Icon
        {
            get { return FtpElement.IconDir; }
        }
    }
}
