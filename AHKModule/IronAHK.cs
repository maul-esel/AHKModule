using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    public class IronAHK : ModuleBase
    {
        public override Guid Language { get { return new Guid("{2db75a0a-eafa-46ad-90b6-1143094e3ed1}"); } }
        public override string LanguageName { get { return "AutoHotkey_L"; } }
        public override ImageSource Icon { get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/iron.png")).GetAsFrozen() as ImageSource; } }
    }
}
