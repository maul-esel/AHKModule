using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChameleonCoder.LanguageModules;
using ChameleonCoder.Resources;
using ChameleonCoder.Resources.Base;

namespace AHKModule.AutoHotkey
{
    public class AutoHotkey : ILanguageModule
    {
        string ILanguageModule.Author { get { return "maul.esel"; } }
        string ILanguageModule.Version { get { return "1.0"; } }
        string ILanguageModule.About { get { return "Copyright (c) 2011 maul.esel"; } }

        int ILanguageModule.APIVersion { get { return 1; } }
        Guid ILanguageModule.Language { get { return new Guid("{f67fb9f3-ea9c-4140-b960-f47a44b5de56}"); } }
        string ILanguageModule.LanguageName { get { return "AutoHotkey"; } }

        bool ILanguageModule.SupportsClasses { get { return false; } }
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
            this._icon = Imaging.CreateBitmapSourceFromHBitmap(Images.AHKB.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        void ILanguageModule.Shutdown()
        {
        }

        void ILanguageModule.Load()
        {
            this.Host.AddButton("MsgBoxCreator", null, LoadMsgBoxCreator, 2);
        }

        void ILanguageModule.Unload()
        {
        }

        void ILanguageModule.CharTyped(Guid resource, char typed)
        {
        }

        void ILanguageModule.Compile(Guid resourceID)
        {
            ResourceBase resource = Host.GetResource(resourceID);

            while (resource is LinkResource)
                resource = (resource as LinkResource).Resolve();

            switch (resource.Type)
            {
                case ResourceType.file: resource = resource as FileResource; break;
                case ResourceType.code: resource = resource as CodeResource; break;
                case ResourceType.library: resource = resource as LibraryResource; break;
                case ResourceType.project: resource = resource as ProjectResource; break;
                case ResourceType.task: resource = resource as TaskResource; break;
            }

            if (resource != null)
            {
                //Compiler = new CompileAHK<T>();
                // todo: make CompileAHK generic???
            }
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

        #region MsgBoxCreator
        static MsgBoxCreator MsgBoxCreator;

        public void LoadMsgBoxCreator(object sender, System.Windows.RoutedEventArgs e)
        {
            MsgBoxCreator = new MsgBoxCreator(MsgBoxCreator_InsertCode, null);
            
            MsgBoxCreator.ShowDialog();
        }

        internal void MsgBoxCreator_InsertCode()
        {
            this.Host.InsertCode(MsgBoxCreator.Code);
        }
        #endregion
    }
}
