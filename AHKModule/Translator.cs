using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.Text;

namespace AHKModule
{
    class Translator
    {
        private static ResourceManager resMgr;

        [Obsolete("wrong namespace", true)]
        internal static void UpdateLanguage(string langID)
        {
            try
            {
                //Set Language  
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langID);

                // Init ResourceManager
                resMgr = new ResourceManager("ChameleonCoder.Localizer", Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "update");
            }
        }

        internal static string get_string(string pattern)
        {
            string result = string.Empty;
            try
            {
                result = resMgr.GetString(pattern);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "get");
            }
            return result;
        }
    }
}
