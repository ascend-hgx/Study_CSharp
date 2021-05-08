using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace study.NewFolder1.Right.Basic
{
    public delegate void MyEventHandler(object sender, string e);

    public partial class Title : UserControl
    {
        public Title()
        {
            InitializeComponent();
        }

        public Title(string str)
        {
            InitializeComponent();
            bar.Text = str;
        }

        public bool show = true; 
        public event MyEventHandler Changed;

        private void Title_Click(object sender, EventArgs e)
        {
            if (show)
            {
                this.pictureBox1.Image = global::Study_CSharp.Properties.Resources._1073328;
                show = false;
            }
            else
            {
                this.pictureBox1.Image = global::Study_CSharp.Properties.Resources._1073317;
                show = true;
            }
            Changed(show, this.bar.Text);
        }
    }
}
