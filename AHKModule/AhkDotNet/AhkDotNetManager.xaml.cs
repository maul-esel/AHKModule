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
            DataContext = ParseDir(currentUri);
            // create list of files
        }

        Uri currentUri;
        NetworkCredential credentials;

        private void CreateDir(object sender, EventArgs e)
        {
            var box = new ChameleonCoder.Interaction.InputBox("AutoHotkey.NET manager", "Enter the new directory's name.");
            if (box.ShowDialog() == true)
            {
                var dir = new Uri(currentUri, Uri.EscapeDataString(box.Text));
                CreateDir(dir);
            }
        }

        private void CreateDir(Uri directory)
        {
            var request = WebRequest.Create(directory);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.GetResponse();
            request.Abort();
        }

        private void RenameFiles(object sender, EventArgs e)
        {
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

                    request.GetResponse();


                    // todo: save stream
                }
            }
        }

        /// <summary>
        /// deletes the selected files and directories from the FTP server
        /// </summary>
        /// <param name="sender">not used.</param>
        /// <param name="e">not used.</param>
        private void DeleteFiles(object sender, EventArgs e)
        {
            foreach (FtpElement element in list.Items)
            {
                if (element.IsItemChecked)
                {
                    var request = WebRequest.Create(element.ElementUri);
                    request.Credentials = credentials;

                    if (element is FtpDirectory)
                        request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                    else
                        request.Method = WebRequestMethods.Ftp.DeleteFile;

                    request.GetResponse();
                }
            }
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
