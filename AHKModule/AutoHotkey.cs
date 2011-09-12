using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.Highlighting;
using XshdLoader = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader;

namespace AHKModule
{
    [ChameleonCoder.CCPlugin]
    public class AutoHotkey : ModuleBase
    {
        public override string Description
        {
            get { return "the language module for the AutoHotkey scripting language"; }
        }

        public override IHighlightingDefinition Highlighting
        {
            get
            {
                if (definition == null)
                {
                    using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AHKModule.Syntax.AHK.xshd"))
                    using (var reader = System.Xml.XmlReader.Create(stream))
                        definition = XshdLoader.Load(reader, HighlightingManager.Instance);
                }
                return definition;
            }
        }

        public override ImageSource Icon
        {
            get { return new BitmapImage(new Uri("pack://application:,,,/AHKModule;component/Icons/basic.png")).GetAsFrozen() as ImageSource; }
        }

        public override Guid Identifier
        {
            get { return Guid; }
        }

        public override string Name
        {
            get { return "AutoHotkey"; }
        }        

        public static readonly Guid Guid = new Guid("{f67fb9f3-ea9c-4140-b960-f47a44b5de56}");

        private static IHighlightingDefinition definition;
    }
}
