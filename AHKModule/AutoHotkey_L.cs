using System;
using ChameleonCoder.Plugins;

namespace AHKModule
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

        void ILanguageModule.Initalize()
        {
        }

        void ILanguageModule.Shutdown()
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

        string ILanguageModule.NewClass(Guid resource)
        {
            return string.Empty;
        }

        string ILanguageModule.NewFunction(Guid resource)
        {
            return string.Empty;
        }

        string ILanguageModule.NewLabel(Guid resource)
        {
            return string.Empty;
        }
    }
}
