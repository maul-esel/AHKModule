using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows.Media;
using ChameleonCoder.Plugins;

namespace AhkModule.AhkDotNet
{
    [ChameleonCoder.CCPlugin]
    public class AhkDotNetTool : IService
    {
        public AhkDotNetTool() { }

        public string About { get { return "© 2011 maul.esel"; } }

        public string Author { get { return "maul.esel"; } }

        public string Description { get { return "a tool to upload files on autohotkey.net"; } }

        public string Version { get { return "0.1"; } }

        public ImageSource Icon { get { return new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/AhkModule;component/AhkDotNet/autohotkey.net.png")); } }

        public Guid Identifier { get { return new Guid("{98376daa-c496-4f03-aefb-d6385010c0f1}"); } }

        public string Name { get { return "AHK.Net Uploader"; } }

        public void Initialize() { }

        public void Shutdown() { }

        public void Execute()
        {
            var login = new AhkDotNetLogin();
            if (login.ShowDialog() == true)
            {
                using (login.password.SecurePassword)
                {
                    var request = WebRequest.Create("ftp://autohotkey.net/");
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    request.Credentials = new NetworkCredential(login.username.Text, login.password.SecurePassword);

                    try
                    {
                        request.GetResponse();
                    }
                    catch (WebException e)
                    {
                        System.Windows.MessageBox.Show("connection to 'ftp://autohotkey.net/' failed.\n"
                            + "Reason: " + e.Message);
                        return;
                    }
                    finally
                    {
                        request.Abort();
                    }

                    var manager = new AhkDotNetManager(new NetworkCredential(login.username.Text, login.password.SecurePassword.Copy()));
                    manager.ShowDialog();
                }
            }

            /*
            var window = new AhkDotNetWindow();
            if (window.ShowDialog() == false)
                return;

            using (var password = window.Password)
            {
                string username = window.Username;
                if (string.IsNullOrEmpty(username))
                    return;

                bool success = true;
                var files = window.Files;
                foreach (var file in files)
                {
                    WebRequest request = WebRequest.Create(new Uri("ftp://autohotkey.net/" + file.FtpPath));
                    System.Windows.MessageBox.Show(file.FtpPath);
                    try
                    {
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential(username, password);

                        request.GetResponse();

                        using (var stream = request.GetRequestStream())
                        {
                            using (var filestream = new System.IO.FileStream(file.LocalPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                            {
                                filestream.CopyTo(stream);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show(e.ToString());
                        success = false;
                        break;
                    }
                    finally
                    {
                        request.Abort();
                    }
                }
                if (success)
                    System.Windows.MessageBox.Show("complete!");
                else
                    System.Windows.MessageBox.Show("error!");
            }*/
        }

        public bool IsBusy { get; set; }
    }
}
