using System.Windows.Controls;
using System.Windows.Media;

namespace AHKModule
{
    /// <summary>
    /// Interaktionslogik für ImageCheckBox.xaml
    /// </summary>
    public sealed partial class ImageCheckBox : UserControl
    {
        public ImageCheckBox()
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
