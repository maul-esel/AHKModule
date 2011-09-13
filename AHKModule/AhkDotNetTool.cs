using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using ChameleonCoder.Plugins;
using System.Net;

namespace AHKModule
{
    [ChameleonCoder.CCPlugin]
    public class AhkDotNetTool : IService
    {
        public AhkDotNetTool() { }

        public string About { get { return "© 2011 maul.esel"; } }

        public string Author { get { return "maul.esel"; } }

        public string Description { get { return "a tool to upload files on autohotkey.net"; } }

        public string Version { get { return "0.1"; } }

        public ImageSource Icon { get { return new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/AHKModule;component/autohotkey.net.png")); } }

        public Guid Identifier { get { return new Guid("{98376daa-c496-4f03-aefb-d6385010c0f1}"); } }

        public string Name { get { return "AHK.Net"; } }

        public void Initialize() { }

        public void Shutdown() { }

        public void Execute()
        {
            var window = new AhkDotNetWindow();
            if (window.ShowDialog() == false)
                return;

            string username = window.Username;
            string password = window.Password;

            var files = window.Files;
            foreach (var file in files)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://autohotkey.net/" + "mytestFile.xyz");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);

                try
                {
                    request.GetResponse();
                }
                catch (Exception)
                {
                    // report error
                    return;
                }
                // report success

                try
                {
                    var stream = request.GetRequestStream();
                    using (var filestream = new System.IO.FileStream(@"C:\Users\Dominik\galettes.txt", System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        filestream.CopyTo(stream);
                    }
                    stream.Close();
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.ToString());
                }
            }
     
            request.Abort();
        }

        public bool IsBusy { get; set; }
    }
}
