using System;
using System.IO;
using System.Windows;
using System.Drawing;
using Forms = System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace AHKModule
{
    internal delegate void MsgBoxCreatorEvent();
    /// <summary>
    /// Interaktionslogik für MsgBoxCreator.xaml
    /// </summary>
    internal partial class MsgBoxCreator : Window
    {
        internal string Code { get; private set; }

        MsgBoxCreatorEvent Insert, Save;
        string title, text;
        int options, timeout;

        internal MsgBoxCreator(MsgBoxCreatorEvent onInsert, MsgBoxCreatorEvent onSave)
        {
            InitializeComponent();
            this.icon_exclamation.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Exclamation.Handle, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(50, 50));
            this.icon_hand.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Hand.Handle, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(50, 50));
            this.icon_info.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Information.Handle, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(50, 50));
            this.icon_question.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Question.Handle, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(50, 50));

            Insert = onInsert; Save = onSave;
        }

        private void CreateCode(object sender, EventArgs e)
        {
            int flags = 0;
            switch (this.modal_Combo.SelectedIndex)
            {
                case 1: flags += 8192;
                    break;
                case 2: flags += 4096;
                    break;
                case 3: flags += 262144;
                    break;
                case 4: flags += 131072;
                    break;
                default:
                    flags += 0;
                    break;
            }

            if (this.icon_none.IsChecked == true)
            {
                flags += 0;
            }
            else if (this.icon_hand.IsChecked == true)
            {
                flags += 16;
            }
            else if (this.icon_question.IsChecked == true)
            {
                flags += 32;
            }
            else if (this.icon_exclamation.IsChecked == true)
            {
                flags += 48;
            }
            else if (this.icon_info.IsChecked == true)
            {
                flags += 64;
            }

            switch (this.button_Combo.SelectedIndex)
            {
                case 1: flags += 1;
                    break;
                case 2: flags += 2;
                    break;
                case 3: flags += 3;
                    break;
                case 4: flags += 4;
                    break;
                case 5: flags += 5;
                    break;
                case 6: flags += 6;
                    break;
                default: flags += 0;
                    break;
            }

            switch (this.defbutton_Combo.SelectedIndex)
            {
                case 1: flags += 256;
                    break;
                case 2: flags += 512;
                    break;
                default: flags += 0;
                    break;
            }

            if (this.HelpButton.IsChecked == true)
                flags += 16384;

            if (this.right_Check.IsChecked == true)
            {
                flags += 524288;
            }

            if (this.rtl_Check.IsChecked == true)
            {
                flags += 1048576;
            }
            this.options = flags;
            this.title = this.title_Text.Text;
            this.text = this.text_Text.Text;
            //this.timeout = (int)this.numericUpDown1.Value;

            System.Text.StringBuilder builder = new System.Text.StringBuilder(this.title_Text.Text);
            builder.Replace("\n", "`n");
            builder.Replace(",", "`,");
            builder.Replace(";", "`;");
            string title = builder.ToString();

            this.Code = this.output_Text.Text = "MsgBox " + flags + ", " + title + ", " + this.text + ", " + this.timeout;
        }

        private void InsertCode(object sender, EventArgs e)
        {
            if (this.Insert != null)
                this.Insert();
        }

        private void SaveCode(object sender, EventArgs e)
        {
            if (this.Save != null)
                this.Save();
        }

        private void TestCode(object sender, EventArgs e)
        {
            this.CreateCode(null, null);
            this.MsgBox(this.options, this.title, this.text, this.timeout);
        }

        /// <summary>
        /// parses code and creates the MsgBox.
        /// This code is taken from the IronAHK project: www.ironahk.net
        /// </summary>
        /// <param name="Options"></param>
        /// <param name="Title"></param>
        /// <param name="Text"></param>
        /// <param name="Timeout"></param>
        private void MsgBox(int Options, string Title, string Text, int Timeout)
        {
            Forms.MessageBoxButtons buttons = Forms.MessageBoxButtons.OK;

            switch (Options & 0xf)
            {
                case 0: buttons = Forms.MessageBoxButtons.OK; break;
                case 1: buttons = Forms.MessageBoxButtons.OKCancel; break;
                case 2: buttons = Forms.MessageBoxButtons.AbortRetryIgnore; break;
                case 3: buttons = Forms.MessageBoxButtons.YesNoCancel; break;
                case 4: buttons = Forms.MessageBoxButtons.YesNo; break;
                case 5: buttons = Forms.MessageBoxButtons.RetryCancel; break;
            }

            Forms.MessageBoxIcon icon = Forms.MessageBoxIcon.None;
            switch (Options & 0xf0)
            {
                case 16: icon = Forms.MessageBoxIcon.Hand; break;
                case 32: icon = Forms.MessageBoxIcon.Question; break;
                case 48: icon = Forms.MessageBoxIcon.Exclamation; break;
                case 64: icon = Forms.MessageBoxIcon.Asterisk; break;
            }

            Forms.MessageBoxDefaultButton defaultbutton = Forms.MessageBoxDefaultButton.Button1;
            switch (Options & 0xf00)
            {
                case 256: defaultbutton = Forms.MessageBoxDefaultButton.Button2; break;
                case 512: defaultbutton = Forms.MessageBoxDefaultButton.Button3; break;
            }

            var options = default(Forms.MessageBoxOptions);
            switch (Options & 0xf0000)
            {
                case 131072: options = Forms.MessageBoxOptions.DefaultDesktopOnly; break;
                case 262144: options = Forms.MessageBoxOptions.ServiceNotification; break;
                case 524288: options = Forms.MessageBoxOptions.RightAlign; break;
                case 1048576: options = Forms.MessageBoxOptions.RtlReading; break;
            }

            bool help = (Options & 0xf000) == 16384;

            if (string.IsNullOrEmpty(Title))
            {
                var script = Environment.GetEnvironmentVariable("SCRIPT");

                if (!string.IsNullOrEmpty(script) && File.Exists(script))
                    Title = Path.GetFileName(script);
            }

            Forms.MessageBox.Show(Text, Title, buttons, icon, defaultbutton, options, help);
        }
    }
}
