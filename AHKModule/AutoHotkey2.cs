using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.Highlighting;
using XshdLoader = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader;

namespace AhkModule
{
    [ChameleonCoder.CCPlugin]
    public class AutoHotkey2 : ModuleBase
    {
        public override IHighlightingDefinition Highlighting
        {
            get
            {
                if (definition == null)
                {
                    using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AhkModule.Syntax.AHK_2.xshd"))
                    using (var reader = System.Xml.XmlReader.Create(stream))
                        definition = XshdLoader.Load(reader, HighlightingManager.Instance);
                }
                return definition;
            }
        }

        public override string Description
        {
            get { return "the language module for the AutoHotkey v2 scripting language"; }
        }

        public override ImageSource Icon
        {
            get { return new BitmapImage(new Uri("pack://application:,,,/AhkModule;component/Icons/version2.png")).GetAsFrozen() as ImageSource; }
        }

        public override Guid Identifier
        {
            get { return Guid; }
        }

        public override string Name
        {
            get { return "AutoHotkey v2"; }
        }


        public static readonly Guid Guid = new Guid("{17176460-6d4b-4cca-b64d-b51ac96ab584}");
        private static IHighlightingDefinition definition;
    }
}
