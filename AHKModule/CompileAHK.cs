using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AHKModule
{
    internal partial class CompileAHK : Form
    {
        internal string fileIn { get; set; }
        internal string fileOut { get; set; }
        internal string icon { get; set; }
        internal string password { get; set; }
        internal Guid AHKVersion { get; set; }

        internal CompileAHK(string fileIn, string fileOut, string icon, string password, Guid AHKVersion)
        {
            this.fileIn = fileIn;
            this.fileOut = fileOut;
            this.icon = icon;
            this.password = password;
            this.AHKVersion = AHKVersion;
            InitializeComponent();
            
            // todo: one parameter (the resource to compile), convert to IResource or cFile or ...

            this.listView1.Columns.Add(string.Empty);
            this.listView1.Columns.Add(string.Empty);
            this.listView1.Columns.Add(string.Empty);

            this.UpdateLanguage();

            this.comboBox_compression.SelectedIndex = 0;
        }

        internal void UpdateLanguage()
        {
            this.tabPage1.Text = Translator.get_string("panel1");
            this.tabPage2.Text = Translator.get_string("panel2");
            this.tabPage3.Text = Translator.get_string("panel3");
            this.tabPage4.Text = Translator.get_string("panel4");

            this.label_outExe.Text = Translator.get_string("out_exe");
            this.label_compression.Text = Translator.get_string("compression");
            this.label_password.Text = Translator.get_string("password");
            this.label_runbefore.Text = Translator.get_string("runbefore");
            this.label_runafter.Text = Translator.get_string("runafter");
            this.label_UAC.Text = Translator.get_string("UAC");

            int compression = this.comboBox_compression.SelectedIndex;

            this.comboBox_compression.Items.Clear();
            this.comboBox_compression.Items.Add(Translator.get_string("compression_none"));
            this.comboBox_compression.Items.Add(Translator.get_string("compression_lowest"));
            this.comboBox_compression.Items.Add(Translator.get_string("compression_low"));
            this.comboBox_compression.Items.Add(Translator.get_string("compression_normal"));
            this.comboBox_compression.Items.Add(Translator.get_string("compression_high"));
            this.comboBox_compression.Items.Add(Translator.get_string("compression_highest"));

            this.comboBox_compression.SelectedIndex = compression;

            this.listView1.Columns[0].Text = Translator.get_string("Col_Resource");
            this.listView1.Columns[1].Text = Translator.get_string("Col_Target");
            this.listView1.Columns[2].Text = Translator.get_string("Col_Folder");
        }

        private void Compile()
        {
            StringBuilder cmd = new StringBuilder("/in {0} /out {1} /icon {2} /password {3}");
            cmd.Replace("{0}", "\"" + this.fileIn + "\"");
            cmd.Replace("{1}", "\"" + this.fileOut + "\"");
            cmd.Replace("{2}", "\"" + this.icon + "\"");
            cmd.Replace("{3}", "\"" + this.password + "\"");
            string cmd_ = cmd.ToString();
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Plugins\\" + AHKVersion.ToString() + "\\Compiler.exe",
                cmd_);

        }
    }
}
