using System;
using ChameleonCoder.Plugins;

namespace AHKModule
{
    public class AutoHotkey_2 : ILanguageModule
    {
        string ILanguageModule.Author { get { return "maul.esel"; } }
        string ILanguageModule.Version { get { return "1.0"; } }
        string ILanguageModule.About { get { return "Copyright (c) 2011 maul.esel"; } }

        int ILanguageModule.APIVersion { get { return 1; } }
        Guid ILanguageModule.Language { get { return new Guid("{17176460-6d4b-4cca-b64d-b51ac96ab584}"); } }
        string ILanguageModule.LanguageName { get { return "AutoHotkey v2"; } }

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
