using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
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

        private void DownloadSelected(object sender, EventArgs e)
        {
            string dir = null;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dir = dialog.SelectedPath;
                }
            }

            if (dir != null)
            {
                foreach (FtpElement element in list.Items)
                {
                    if (element.IsItemChecked)
                    {
                        MessageBox.Show(element.ElementUri.ToString() + " ==> " + dir);
                        if (element is FtpFile)
                        {
                            DownloadFile(element as FtpFile, dir);
                        }
                        else if (element is FtpDirectory)
                        {
                            DownloadDir(element as FtpDirectory, dir);
                        }
                    }
                }
            }
            MessageBox.Show("complete!");
        }

        private void DownloadDir(FtpDirectory dir, string root)
        {
            string dirPath = Path.Combine(root, dir.Name);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            MessageBox.Show(dir.ElementUri.ToString() + " == " + dir.Name);
            foreach (FtpElement element in ParseDir(dir.ElementUri))
            {
                MessageBox.Show(element.ElementUri.ToString() + " ==> " + dirPath);
                if (element is FtpFile)
                {
                    DownloadFile(element as FtpFile, dirPath);
                }
                else if (element is FtpDirectory)
                {
                    DownloadDir(element as FtpDirectory, dirPath);
                }
                MessageBox.Show("one more done");
            }
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

        private void DownloadFile(FtpFile file, string root)
        {
            var request = WebRequest.Create(file.ElementUri);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            MessageBox.Show("downloading: " + Path.Combine(root, file.Name) + "\nfrom: " + file.ElementUri);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    string path = Path.Combine(root, file.Name);
                    FileStream fileStream = null;

                    if (!File.Exists(path))
                        fileStream = File.Create(path);
                    else
                        fileStream = File.OpenWrite(path);

                    using (fileStream)
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }
        }

        private void RenameItem(FtpElement item, string name)
        {
            var request = (FtpWebRequest)WebRequest.Create(item.ElementUri);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = name;

            using (request.GetResponse()) { }
        }

        /// <summary>
        /// gets FtpElement instances for all files in a given FTP directory
        /// </summary>
        /// <param name="root">the URI of the directory</param>
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
                    using (var reader = new StreamReader(stream))
                    {
                        string[] data = reader.ReadToEnd().Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in data)
                        {
                            var element = FtpElement.Open(line, dir);
                            if (element != null)
                                list.Add(element);
                        }
                    }
                }
            }

            return list;
        }

        #endregion

        private void Update()
        {
            DataContext = ParseDir(currentUri);
        }
    }
}
