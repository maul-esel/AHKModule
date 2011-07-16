using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    public class AutoHotkey2 : ModuleBase
    {
        public override Guid Language { get { return new Guid("{17176460-6d4b-4cca-b64d-b51ac96ab584}"); } }
        public override string LanguageName { get { return "AutoHotkey_L"; } }
        public override ImageSource Icon { get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/version2.png")); } }
    }
}
