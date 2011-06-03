using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKScriptsMan.Plugins.AHK
{
    public delegate void LanguageEvent(Guid language, object[] Arguments);
    public struct PluginInfo
    {
        public Guid language;
        public string languageName;
        public LanguageEvent CompilerCallback;
        public LanguageEvent ExecutionCallback;
        public LanguageEvent CharTypedCallback;
    }
}
