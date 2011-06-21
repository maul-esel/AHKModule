﻿using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChameleonCoder.Plugins;

namespace AHKModule.AutoHotkey_L
{
    public class AutoHotkey_L : ILanguageModule
    {
        string ILanguageModule.Author { get { return "maul.esel"; } }
        string ILanguageModule.Version { get { return "1.0"; } }
        string ILanguageModule.About { get { return "Copyright (c) 2011 maul.esel"; } }

        int ILanguageModule.APIVersion { get { return 1; } }
        Guid ILanguageModule.Language { get { return new Guid("{c3da3961-e7ef-4ec2-9576-63a91e0916cb}"); } }
        string ILanguageModule.LanguageName { get { return "AutoHotkey_L"; } }

        bool ILanguageModule.SupportsClasses { get { return true; } }
        bool ILanguageModule.SupportsFunctions { get { return true; } }
        bool ILanguageModule.SupportsLabels { get { return true; } }

        bool ILanguageModule.IsBusy { get { return _busy; } }

        ImageSource ILanguageModule.Icon { get { return _icon; } }

        private bool _busy;

        private ImageSource _icon;

        private ILanguageModuleHost Host;

        void ILanguageModule.Initialize(ILanguageModuleHost host)
        {
            this.Host = host;
            this._icon = Imaging.CreateBitmapSourceFromHBitmap(Images.AHK2.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        void ILanguageModule.Shutdown()
        {
        }

        void ILanguageModule.Load()
        {
        }

        void ILanguageModule.Unload()
        {
        }

        void ILanguageModule.CharTyped(Guid resource, char typed)
        {
        }

        void ILanguageModule.Compile(Guid resource)
        {
        }

        void ILanguageModule.Execute(Guid resource)
        {
        }

        bool ILanguageModule.NewClass(Guid resource, out string code)
        {
            code = string.Empty;
            return false;
        }

        bool ILanguageModule.NewFunction(Guid resource, out string code)
        {
            code = string.Empty;
            return false;
        }

        bool ILanguageModule.NewLabel(Guid resource, out string code)
        {
            code = string.Empty;
            return false;
        }
    }
}