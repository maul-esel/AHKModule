using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;

namespace AhkModule.AhkDotNet
{
    /// <summary>
    /// Interaktionslogik für AhkDotNetManager.xaml
    /// </summary>
    internal sealed partial class AhkDotNetManager : Window
    {
        public AhkDotNetManager(NetworkCredential credentials)
        {
            this.credentials = credentials;
            currentUri = new Uri("ftp://autohotkey.net/");

            InitializeComponent();
            Update();
        }

        Uri currentUri;
        NetworkCredential credentials;

        #region handlers for new objects
        /*
         * This region contains event handlers for buttons creating new FtpElements
         */

        private void CreateDir(object sender, EventArgs e)
        {
            var box = new ChameleonCoder.Interaction.InputBox("AutoHotkey.NET manager", "Enter the new directory's name.");
            if (box.ShowDialog() == true)
            {
                CreateDir(new Uri(currentUri, Uri.EscapeDataString(box.Text)));
                Update();
            }
        }

        #endregion

        #region handlers for modification of existing objects
        /*
         * This region contains event handlers that modify existing (selected) FtpElements
         */

        private void DeleteSelected(object sender, EventArgs e)
        {
            foreach (FtpElement element in list.Items)
            {
                if (element.IsItemChecked)
                {
                    DeleteItem(element);
                }
            }
            Update();
        }

        private void RenameSelected(object sender, EventArgs e)
        {
            foreach (FtpElement element in list.Items)
            {
                if (element.IsItemChecked)
                {
                    var box = new ChameleonCoder.Interaction.InputBox("AutoHotkey.NET manager", string.Format("Enter the new name for '{0}'.", element.Name)); if (box.ShowDialog() == true)
                    {
                        RenameItem(element, box.Text);
                    }
                }
            }
            Update();
        }

        private void DownloadFiles(object sender, EventArgs e)
        {
            foreach (FtpElement element in list.Items)
            {
                if (element.IsItemChecked)
                {
                    var request = WebRequest.Create(element.ElementUri);
                    request.Credentials = credentials;

                    if (element is FtpDirectory)
                    {
                        // todo: download included files
                        return;
                    }
                    else
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                    using (request.GetResponse()) { }


                    // todo: save stream
                }
            }
            Update();
        }

        #endregion

        #region FTP implementations
        /*
         * This region contains the methods that actually interact with the FTP server
         */

        private void CreateDir(Uri directory)
        {
            var request = WebRequest.Create(directory);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.MakeDirectory;

            using (request.GetResponse()) { }
        }

        private void DeleteItem(FtpElement item)
        {
            var request = WebRequest.Create(item.ElementUri);
            request.Credentials = credentials;

            if (item is FtpDirectory)
                request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            else
                request.Method = WebRequestMethods.Ftp.DeleteFile;

            using (request.GetResponse()) { }
        }

        private void RenameItem(FtpElement item, string name)
        {
            var request = (FtpWebRequest)WebRequest.Create(item.ElementUri);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = name;

            using (request.GetResponse()) { }
        }

        #endregion

        private void Update()
        {
            DataContext = ParseDir(currentUri);
        }

        /// <summary>
        /// gets FtpElement instances for all files in a given FTP directory
        /// </summary>
        /// <param name="dir">the URI of the directory</param>
        /// <returns>an IList&lt;FtpElement&gt; instance containing the FtpElement instances</returns>
        private IList<FtpElement> ParseDir(Uri dir)
        {
            var list = new List<FtpElement>();

            FtpWebRequest request = WebRequest.Create(dir) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = credentials;

            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        string[] data = reader.ReadToEnd().Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in data)
                        {
                            var element = FtpElement.Open(line, currentUri);
                            if (element != null)
                                list.Add(element);
                        }
                    }
                }
            }

            return list;
        }
    }
}
