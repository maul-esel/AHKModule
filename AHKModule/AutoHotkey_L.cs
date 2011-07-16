using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    public class AutoHotkey_L : ModuleBase
    {
        public override Guid Language { get { return new Guid("{c3da3961-e7ef-4ec2-9576-63a91e0916cb}"); } }
        public override string LanguageName { get { return "AutoHotkey_L"; } }
        public override ImageSource Icon { get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/lexikos.png")); } }
    }
}
