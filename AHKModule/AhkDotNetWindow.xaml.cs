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

namespace AhkModule
{
    /// <summary>
    /// Interaktionslogik für AhkDotNetWindow.xaml
    /// </summary>
    internal sealed partial class AhkDotNetWindow : Window
    {
        internal AhkDotNetWindow()
        {
            InitializeComponent();
        }

        internal string Username
        {
            get;
            private set;
        }

        internal string Password
        {
            get;
            private set;
        }

        internal IEnumerable<string> Files
        {
            get;
            private set;
        }
    }
}
