using System;
using System.Windows;

namespace AhkModule.AhkDotNet
{
    /// <summary>
    /// Interaktionslogik für AhkDotNetLogin.xaml
    /// </summary>
    internal sealed partial class AhkDotNetLogin : Window
    {
        public AhkDotNetLogin()
        {
            InitializeComponent();
        }

        private void FinishDialog(object sender, EventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
