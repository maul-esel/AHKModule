using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    [ChameleonCoder.CCPlugin]
    public class AutoHotkey_L : ModuleBase
    {
        public override Guid Identifier { get { return GUID; } }
        public override string Name { get { return "AutoHotkey_L"; } }
        public override ImageSource Icon { get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/lexikos.png")).GetAsFrozen() as ImageSource; } }
        public override string Description { get { return "the language module for the AutoHotkey_L scripting language"; } }

        public static readonly Guid GUID = new Guid("{c3da3961-e7ef-4ec2-9576-63a91e0916cb}");
    }
}
