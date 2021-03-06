﻿using System;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AhkModule.AhkDotNet
{
    internal abstract class FtpElement
    {
        protected FtpElement(Match data, Uri directory)
        {
            Directory = directory;

            Name = data.Groups["Name"].Value;

            string date = data.Groups["Day"].Value + "/" + data.Groups["Month"].Value + "/" + DateTime.Now.Year + " " + data.Groups["Hours"].Value + ":" + data.Groups["Minutes"].Value;
            LastModified = DateTime.Parse(date).ToString();

            Size = Math.Round(Int32.Parse(data.Groups["Bytes"].Value) / 1024.00, 2);
        }

        public string Description { get; protected set; }

        protected Uri Directory { get; private set; }

        internal abstract Uri ElementUri { get; }

        public abstract ImageSource Icon { get; }

        public bool IsItemChecked { get; set; }

        public string LastModified { get; protected set; }

        public string Name { get; private set; }

        public double Size { get; protected set; }


        static Regex regEx = new Regex(@"^(?<Type>d|-|l)((r|-)(w|-)(x|-)){3}\s+\d\s+\d{2}\s+([^\s]+)\s+(?<Bytes>\d+)\s+(?<Month>Jan|Feb|Mar|May|Apr|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s+(?<Day>\d{1,2})\s+(?<Hours>\d{2}):(?<Minutes>\d{2})\s+(?<Name>.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture);

        internal static FtpElement Open(string line, Uri directory)
        {
            var match = regEx.Match(line);
            if (match.Success)
            {
                string name = match.Groups["Name"].Value;
                if (name == "." || name == ".." || name == ".ftpquota")
                    return null;

                string type = match.Groups["Type"].Value;
                if (type == "d") // if it is a directory
                    return new FtpDirectory(match, directory);
                else if (type == "-")
                    return new FtpFile(match, directory);
            }
            return null;
        }

        

        private static readonly ImageSource fileIcon = new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/AhkDotNet/file.png"));

        private static readonly ImageSource dirIcon = new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/AhkDotNet/folder.png"));

        protected static ImageSource IconFile
        {
            get { return fileIcon.Clone(); }
        }

        protected static ImageSource IconDir
        {
            get { return dirIcon.Clone(); }
        }
    }
}
