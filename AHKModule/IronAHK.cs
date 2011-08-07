﻿using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    [ChameleonCoder.CCPlugin]
    public class IronAHK : ModuleBase
    {
        public override Guid Identifier { get { return GUID; } }
        public override string Name { get { return "AutoHotkey_L"; } }
        public override ImageSource Icon { get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/iron.png")).GetAsFrozen() as ImageSource; } }
        public override string Description { get { return "the language module for the IronAHK scripting language"; } }

        public static readonly Guid GUID = new Guid("{2db75a0a-eafa-46ad-90b6-1143094e3ed1}");
    }
}
