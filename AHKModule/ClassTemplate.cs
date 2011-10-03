using System;
using System.Collections.Generic;
using System.Windows.Media;
using ChameleonCoder.Plugins;
using ChameleonCoder.Shared;
using ChameleonCoder.Resources.Management;
using ChameleonCoder.Resources.Interfaces;

namespace AhkModule
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
            get { return typeof(ChameleonCoder.ComponentCore.Resources.CodeResource); }
        }

        public string Group
        {
            get { return "AutoHotkey v1.1 / v2"; }
        }

        public IResource Create(IResource parent, string name)
        {
            i++;
            var dict = ResourceTypeManager.GetFactory(ResourceType).CreateResource(ResourceType, name, parent);

            // get parent's directory
            // create file there, using the name
            // add text to it
            // set the 'path' property in dict

            var resource = ResourceTypeManager.CreateNewResource(ResourceType, name, dict, parent);

            // open the resource in edit mode

            return resource;
        }

        int i = 1;
    }
}
