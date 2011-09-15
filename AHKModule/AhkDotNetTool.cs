using System;
using System.Net;
using System.Windows.Media;
using ChameleonCoder.Plugins;

namespace AhkModule
{
    [ChameleonCoder.CCPlugin]
    public class AhkDotNetTool : IService
    {
        public AhkDotNetTool() { }

        public string About { get { return "© 2011 maul.esel"; } }

        public string Author { get { return "maul.esel"; } }

        public string Description { get { return "a tool to upload files on autohotkey.net"; } }

        public string Version { get { return "0.1"; } }

        public ImageSource Icon { get { return new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/AhkModule;component/autohotkey.net.png")); } }

        public Guid Identifier { get { return new Guid("{98376daa-c496-4f03-aefb-d6385010c0f1}"); } }

        public string Name { get { return "AHK.Net Uploader"; } }

        public void Initialize() { }

        public void Shutdown() { }

        public void Execute()
        {
            var window = new AhkDotNetWindow();
            if (window.ShowDialog() == false)
                return;

            using (var password = window.Password)
            {
                string username = window.Username;
                if (string.IsNullOrEmpty(username))
                    return;

                var files = window.Files;
                foreach (var file in files)
                {
                    WebRequest request = WebRequest.Create(new Uri("ftp://autohotkey.net/" + file.FtpPath));
                    try
                    {                        
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential(username, password);

                        try
                        {
                            request.GetResponse();
                        }
                        catch (Exception e)
                        {
                            // report error
                            System.Windows.MessageBox.Show(e.ToString());
                            break;
                        }
                        // report success

                        try
                        {
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
                            break;
                        }
                    }
                    finally
                    {
                        request.Abort();
                    }
                }
            }
        }

        public bool IsBusy { get; set; }
    }
}
