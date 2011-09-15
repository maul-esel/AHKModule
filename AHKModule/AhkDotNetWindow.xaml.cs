using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            DataContext = this;
        }

        internal string Username
        {
            get { return userBox.Text; }
        }

        internal System.Security.SecureString Password
        {
            get { return pwBox.SecurePassword; }
        }

        public ObservableCollection<UploadFile> Files
        {
            get { return files; }
        }

        private static readonly ObservableCollection<UploadFile> files = new ObservableCollection<UploadFile>();

        private void AddFiles(object sender, EventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog() { Multiselect = true })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (string file in dialog.FileNames)
                    {
                        files.Add(new UploadFile() { LocalPath = file, FtpPath = System.IO.Path.GetFileName(file) });
                    }
                    DataContext = this;
                }
            }
        }

        private void RemoveFiles(object sender, EventArgs e)
        {
            var removeFiles = new System.Collections.Generic.List<UploadFile>();

            foreach (var item in fileList.Items)
            {
                var ctrl = (DependencyObject)fileList.ItemContainerGenerator.ContainerFromItem(item);
                for (int i = 0; i < 6; i++)
                    ctrl = VisualTreeHelper.GetChild(ctrl, i == 3 ? 1 : 0);

                var check = ctrl as CheckBox;
                if (check != null)
                    if (check.IsChecked == true)
                        removeFiles.Add((UploadFile)item);
            }

            foreach (var item in removeFiles)
                files.Remove(item);
        }

        private void FinishDialog(object sender, EventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
