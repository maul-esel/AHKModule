using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICSharpCode.AvalonEdit.Highlighting;
using XshdLoader = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader;

namespace AhkModule
{
    [ChameleonCoder.CCPlugin]
    public class AutoHotkey_L : ModuleBase
    {
        public override string Description
        {
            get { return "the language module for the AutoHotkey_L scripting language"; }
        }

        public override IHighlightingDefinition Highlighting
        {
            get
            {
                if (definition == null)
                {
                    using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AHKModule.Syntax.AHK_L.xshd"))
                        using (var reader = System.Xml.XmlReader.Create(stream))
                            definition = XshdLoader.Load(reader, HighlightingManager.Instance);
                }
                return definition;
            }
        }

        public override ImageSource Icon
        {
            get { return new BitmapImage(new Uri("pack://application:,,,/AhkModule;component/Icons/lexikos.png")).GetAsFrozen() as ImageSource; }
        }

        public override Guid Identifier
        {
            get { return Guid; }
        }

        public override string Name
        {
            get { return "AutoHotkey_L"; }
        } 

        public static readonly Guid Guid = new Guid("{c3da3961-e7ef-4ec2-9576-63a91e0916cb}");

        private static IHighlightingDefinition definition;
    }
}
