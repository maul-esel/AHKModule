using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AHKScriptsMan.Plugins.AHK
{
    internal enum ResourceType
    {
        resource,
        file,
        code,
        library,
        project,
        task
    }

    static class AutoHotkeyPlugin
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static PluginInfo[] Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PluginInfo[] languages = new PluginInfo[4];

            languages[0].language = new Guid("{926d62ed-678d-48b3-b13d-f92293f638d4}");
            languages[0].languageName = "AutoHotkey (basic)";
            languages[0].CompilerCallback += AutoHotkey.Compile;

            languages[1].language = new Guid("{0ce32868-a9d3-4f8a-b51a-06688ae44a10}");
            languages[1].languageName = "AutoHotkey_L";

            languages[2].language = new Guid("{6d7043de-3c15-411a-b4bb-6d9a6dfc5723}");
            languages[2].languageName = "IronAHK";

            languages[3].language = new Guid("{82ff6b49-18fd-42ba-bd1e-e150af60e0a5}");
            languages[3].languageName = "AutoHotkey v2";

            return languages;
        }
    }
}
