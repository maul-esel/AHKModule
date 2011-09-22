using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;

namespace AhkModule.AhkDotNet
{
    /// <summary>
    /// the main window for the AhkDotNetTool service
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

        private Uri currentUri;
        private NetworkCredential credentials;

        #region event handlers for navigation
        /*
         * This region contains event handlers that let the user navigate through directories
         */

        private void OpenDirectory(object sender, EventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext as FtpElement;
            if (item != null && item is FtpDirectory)
            {
                currentUri = item.ElementUri;
                Update();
            }
        }

        private void OpenParent(object sender, EventArgs e)
        {
            // get the URI of the parent directory
            currentUri = new Uri(currentUri.AbsoluteUri.Remove(currentUri.AbsoluteUri.Length - currentUri.Segments[currentUri.Segments.Length - 1].Length));
            Update();
        }

        #endregion

        #region handlers for new objects
        /*
         * This region contains event handlers for buttons creating new FtpElements
         */

        private void CreateDir(object sender, EventArgs e)
        {
            var box = new ChameleonCoder.Interaction.InputBox("AutoHotkey.NET manager", "Enter the new directory's name.");
            if (box.ShowDialog() == true)
            {
                string name = Uri.EscapeDataString(box.Text);
                status.Text = string.Format("creating directory {0} in {1}",
                                            name,
                                            currentUri);

                var bw = new BackgroundWorker();
                bw.DoWork += (s, args) =>
                    {
                        CreateDir(new Uri(currentUri, name));
                    };
                bw.RunWorkerCompleted += (s, args) =>
                    {
                        if (args.Error != null)
                        {
                            status.Text = "error: " + args.Error.Message;
                        }
                        else
                        {
                            status.Text = string.Empty;
                        }
                        Update();
                    };
                bw.RunWorkerAsync();
            }
        }

        #endregion

        #region handlers for modification of existing objects
        /*
         * This region contains event handlers that modify existing (selected) FtpElements
         */

        private void DeleteSelected(object sender, EventArgs e)
        {
            var elements = new System.Collections.Stack(list.Items);
            status.Text = string.Format("deleting files and directories from {0}",
                                        currentUri);
            string log = null;

            var bw = new BackgroundWorker();
            bw.DoWork += (s, args) =>
                {
                    foreach (FtpElement element in elements)
                    {
                        if (element.IsItemChecked)
                        {
                            try
                            {
                                DeleteItem(element);
                            }
                            catch (WebException ex)
                            {
                                log += string.Format("failed to delete '{0}': {1}",
                                                    element.Name,
                                                    ex.Message);
                            }
                        }
                    }
                };
            bw.RunWorkerCompleted += (s, args) =>
                {
                    if (args.Error != null)
                    {
                        status.Text = "error: " + args.Error.Message;
                    }
                    else
                    {
                        status.Text = string.Empty;
                        if (!string.IsNullOrWhiteSpace(log))
                        {
                            MessageBox.Show(log, "AutoHotkey.NET Manager");
                        }                        
                    }
                    Update();
                };
            bw.RunWorkerAsync();
        }

        private void MoveSelected(object sender, EventArgs e)
        {
            status.Text = string.Format("moving files and directories in {0}",
                                        currentUri);
            string log = null;

            foreach (FtpElement element in list.Items)
            {
                if (element.IsItemChecked)
                {
                    var box = new ChameleonCoder.Interaction.InputBox("AutoHotkey.NET manager", string.Format("Enter the new name for '{0}'.", element.Name)); if (box.ShowDialog() == true)
                    {
                        try
                        {
                            MoveItem(element, box.Text);
                        }
                        catch (WebException ex)
                        {
                            log += string.Format("failed to move '{0}' to '{1}': {2}",
                                                element.Name,
                                                box.Text,
                                                ex.Message);
                        }
                    }
                }
            }
            Update();

            if (!string.IsNullOrWhiteSpace(log))
                MessageBox.Show(log, "AutoHotkey.NET Manager");
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
                var elements = new System.Collections.Stack(list.Items);
                status.Text = string.Format("downloading files and directories from '{0}' to '{1}'",
                    currentUri,
                    dir);
                string log = null;

                var bw = new BackgroundWorker();
                bw.DoWork += (s, args) =>
                    {
                        foreach (FtpElement element in elements)
                        {
                            if (element.IsItemChecked)
                            {
                                try
                                {
                                    if (element is FtpFile)
                                    {
                                        DownloadFile(element as FtpFile, dir);
                                    }
                                    else if (element is FtpDirectory)
                                    {
                                        DownloadDir(element as FtpDirectory, dir);
                                    }
                                }
                                catch (WebException ex)
                                {
                                    log += string.Format("failed to download '{0}' to '{1}': {2}",
                                                        element.Name,
                                                        dir,
                                                        ex.Message);
                                }
                            }
                        }
                    };
                bw.RunWorkerCompleted += (s, args) =>
                    {
                        if (args.Error != null)
                        {
                            status.Text = "error: " + args.Error.Message;
                        }
                        else
                        {
                            status.Text = string.Empty;
                            MessageBox.Show("complete!");
                            if (!string.IsNullOrWhiteSpace(log))
                            {
                                MessageBox.Show(log, "AutoHotkey.NET Manager");
                            }
                        }
                    };
                bw.RunWorkerAsync();
            }            
        }

        private void DownloadDir(FtpDirectory dir, string root)
        {
            string dirPath = Path.Combine(root, dir.Name);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            foreach (FtpElement element in ParseDir(dir.ElementUri))
            {
                if (element is FtpFile)
                {
                    DownloadFile(element as FtpFile, dirPath);
                }
                else if (element is FtpDirectory)
                {
                    DownloadDir(element as FtpDirectory, dirPath);
                }
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

        private void MoveItem(FtpElement item, string name)
        {
            var uri = item.ElementUri;
            /*
             * This is a simple hack:
             * As the FtpWebRequest does not accept directury urls for renaming,
             * we simple pretend it to be a file by removing the '/' from the end.
             * The autohotkey.net server seems to handle this correctly
             */            
            if (item is FtpDirectory)
                uri = new Uri(uri.ToString().TrimEnd('/'));

            var request = (FtpWebRequest)WebRequest.Create(uri);
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = "/" + name; // prefix it to avoid exceptions when moving into another directory

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
