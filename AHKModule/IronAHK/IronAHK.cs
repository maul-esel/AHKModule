using System;
using ChameleonCoder.Plugins;

namespace AHKModule.IronAHK
{
    public class IronAHK : ILanguageModule
    {
        string ILanguageModule.Author { get { return "maul.esel"; } }
        string ILanguageModule.Version { get { return "1.0"; } }
        string ILanguageModule.About { get { return "Copyright (c) 2011 maul.esel"; } }

        int ILanguageModule.APIVersion { get { return 1; } }
        Guid ILanguageModule.Language { get { return new Guid("{2db75a0a-eafa-46ad-90b6-1143094e3ed1}"); } }
        string ILanguageModule.LanguageName { get { return "IronAHK"; } }

        bool ILanguageModule.SupportsClasses { get { return false; } }
        bool ILanguageModule.SupportsFunctions { get { return true; } }
        bool ILanguageModule.SupportsLabels { get { return true; } }

        private ILanguageModuleHost Host;

        void ILanguageModule.Initialize(ILanguageModuleHost host)
        {
            this.Host = host;
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
