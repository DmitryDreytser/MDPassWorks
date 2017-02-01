using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MDPassWorks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DecriptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dd = new OpenFileDialog();
            dd.DefaultExt = "*.md";
            if (dd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string mdfilename = dd.FileName;

            Compound MD = new Compound(mdfilename);
            MD.SetPassword();
            MD.Save(mdfilename);
        }

        private void EncriptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dd = new OpenFileDialog();
            dd.DefaultExt = "*.md";
            if (dd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string mdfilename = dd.FileName;

            Compound MD = new Compound(mdfilename);
            MD.SetPassword("deltal2011");
            MD.Save(mdfilename);
        }

        private void LoadMoxcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dd = new OpenFileDialog();
            dd.DefaultExt = "*.mxl";
            if (dd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            string mdfilename = dd.FileName;
            byte[] buffer = File.ReadAllBytes(mdfilename);
            
            Moxel mxl = new Moxel();
            mxl.Load(buffer);

            
           

        }
    }
}
