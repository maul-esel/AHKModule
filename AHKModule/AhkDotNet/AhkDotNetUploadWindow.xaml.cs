using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using OFDialog = System.Windows.Forms.OpenFileDialog;
using SDDialog = System.Windows.Forms.FolderBrowserDialog;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AhkModule.AhkDotNet
{
    /// <summary>
    /// Interaktionslogik für AhkDotNetUploadWindow.xaml
    /// </summary>
    internal partial class AhkDotNetUploadWindow : Window
    {
        internal AhkDotNetUploadWindow()
        {
            InitializeComponent();
            DataContext = elementList;
        }

        internal IList<FtpUploadElement> Elements
        {
            get { return elementList; }
        }

        private readonly ObservableCollection<FtpUploadElement> elementList = new ObservableCollection<FtpUploadElement>();


        private void AddFiles(object sender, EventArgs e)
        {
            using (var dialog = new OFDialog() { Multiselect = true })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var file in dialog.FileNames)
                    {
                        elementList.Add(new FtpUploadElement(file, false));
                    }
                }
            }
        }

        private void AddDirectory(object sender, EventArgs e)
        {
            using (var dialog = new SDDialog() { ShowNewFolderButton = false })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    elementList.Add(new FtpUploadElement(dialog.SelectedPath, true));
                }
            }
        }

        private void RemoveElements(object sender, EventArgs e)
        {
            var list = new List<FtpUploadElement>(elementList);
            foreach (FtpUploadElement element in list)
            {
                if (element.IsItemChecked)
                {
                    elementList.Remove(element);
                }
            }
        }

        private void FinishDialog(object sender, EventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
