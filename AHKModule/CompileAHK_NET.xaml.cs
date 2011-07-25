using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChameleonCoder.Resources.Interfaces;
using ChameleonCoder.Resources;

namespace AHKModule
{
    /// <summary>
    /// Interaktionslogik für CompileAHK_NET.xaml
    /// </summary>
    public partial class CompileAHK_NET : Window
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
