using System;
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

        public string Description { get { return Properties.Resources.AhkDotNet_Description; } }

        public string Version { get { return "0.0.0.1"; } }

        public ImageSource Icon { get { return new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/AhkModule;component/AhkDotNet/autohotkey.net.png")); } }

        public Guid Identifier { get { return new Guid("{98376daa-c496-4f03-aefb-d6385010c0f1}"); } }

        public string Name { get { return "AutoHotkey.NET Tool"; } }

        public void Initialize()
        {
            ChameleonCoder.Shared.InformationProvider.LanguageChanged += (v) => Properties.Resources.Culture = new System.Globalization.CultureInfo((int)v);
        }

        public void Shutdown() { }

        public void Execute()
        {
            var login = new AhkDotNetLogin();
            if (login.ShowDialog() == true)
            {
                using (login.password.SecurePassword)
                {
                    var request = WebRequest.Create(new Uri("ftp://autohotkey.net/"));
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    request.Credentials = new NetworkCredential(login.username.Text, login.password.SecurePassword);

                    WebResponse response = null;
                    try
                    {
                        response = request.GetResponse();
                    }
                    catch (WebException e)
                    {
                        System.Windows.MessageBox.Show(string.Format(Properties.Resources.AhkDotNet_ConnectionFailed, "ftp://autohotkey.net/", e.Message));
                        return;
                    }
                    finally
                    {
                        if (response != null)
                            response.Close();
                    }

                    var manager = new AhkDotNetManager(new NetworkCredential(login.username.Text, login.password.SecurePassword.Copy()));
                    manager.ShowDialog();
                }
            }
        }

        public bool IsBusy { get; private set; }
    }
}
