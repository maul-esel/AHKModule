using System;
using System.Collections.Generic;
using System.Windows.Media;
using ChameleonCoder.Plugins;
using ChameleonCoder.Resources.Interfaces;

namespace AHKModule
{
    [ChameleonCoder.CCPlugin]
    public class ClassTemplate : ITemplate
    {
        public string About { get { return "© 2011, maul.esel"; } }

        public string Author { get { return "maul.esel"; } }

        public string Description { get { return "creates a new class"; } }

        public string DefaultName { get { return "class" + i + ".ahk"; } }

        public ImageSource Icon { get { return new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/version2.png")); } }

        public Guid Identifier { get { return new Guid("{6e593e30-6d9d-4639-909e-897c898425da}"); } }

        public string Name { get { return "AHK class template"; } }

        public string Version { get { return "0.0.0.1"; } }

        public void Initialize() { }

        public void Shutdown() { }

        public Type ResourceType
        {
            get { return typeof(ChameleonCoder.ResourceCore.CodeResource); }
        }

        public string Group
        {
            get { return "AutoHotkey v1.1 / v2"; }
        }

        public IEnumerable<Guid> Languages
        {
            get
            {
                return new List<Guid>(new Guid[2] { AutoHotkey_L.GUID, AutoHotkey2.GUID });
            }
        }

        public IResource Create(IResource parent, string name)
        {
            i++;
            return null;
        }

        int i = 1;
    }
}
