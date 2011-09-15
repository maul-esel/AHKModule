using System.Windows;
using ChameleonCoder.Resources;
using ChameleonCoder.Resources.Interfaces;

namespace AhkModule
{
    /// <summary>
    /// Interaktionslogik für CompileAHK_NET.xaml
    /// </summary>
    public sealed partial class CompileAHK_NET : Window
    {
        public CompileAHK_NET(ICompilable resource)
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
