using System;
using ChameleonCoder.Plugins;

namespace AHKModule
{
    public class AutoHotkeyBasic : ILanguageModule
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
