﻿using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    [ChameleonCoder.CCPlugin]
    public class AutoHotkey2 : ModuleBase
    {
        public override Guid Identifier { get { return Guid; } }
        public override string Name { get { return "AutoHotkey v2"; } }
        public override ImageSource Icon { get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/version2.png")).GetAsFrozen() as ImageSource; } }
        public override string Description { get { return "the language module for the AutoHotkey v2 scripting language"; } }

        public static readonly Guid Guid = new Guid("{17176460-6d4b-4cca-b64d-b51ac96ab584}");
    }
}
