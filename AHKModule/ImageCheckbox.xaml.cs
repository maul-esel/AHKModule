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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AHKModule
{
    /// <summary>
    /// Interaktionslogik für ImageCheckbox.xaml
    /// </summary>
    public partial class ImageCheckbox : UserControl
    {
        public ImageCheckbox()
        {
            InitializeComponent();
        }

        public bool? IsChecked
        {
            get { return this.check.IsChecked; }
            set { this.check.IsChecked = value; }
        }

        public ImageSource Source
        {
            get { return this.image.Source; }
            set { this.image.Source = value;}
        }

        public object Content
        {
            get { return this.check.Content; }
            set { this.check.Content = value; }
        }
    }
}
