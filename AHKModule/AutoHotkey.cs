using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    public class AutoHotkey : ModuleBase
    {
        public override Guid Language { get { return new Guid("{f67fb9f3-ea9c-4140-b960-f47a44b5de56}"); } }
        public override string LanguageName { get { return "AutoHotkey"; } }
        public override ImageSource Icon { get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/basic.png")); } }
    }
}
