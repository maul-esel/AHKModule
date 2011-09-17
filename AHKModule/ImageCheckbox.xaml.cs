using System.Windows.Controls;
using System.Windows.Media;

namespace AhkModule
{
    /// <summary>
    /// Interaktionslogik für ImageCheckBox.xaml
    /// </summary>
    [System.Obsolete("use simply CheckBox class and content property")]
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
