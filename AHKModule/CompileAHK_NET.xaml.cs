using System.Windows;
using ChameleonCoder.Resources;

namespace AhkModule
{
    /// <summary>
    /// Interaktionslogik für CompileAHK_NET.xaml
    /// </summary>
    public sealed partial class CompileAHK_NET : Window
    {
        public CompileAHK_NET(IResource resource)
        {
            InitializeComponent();
            this.resources = (ResourceCollection)this.Resources["resources"];

            if (resource != null)
            {
                resources.Add(resource);
            }
        }

        ResourceCollection resources;
    }
}
