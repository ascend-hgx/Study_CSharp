using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using study.NewFolder1.Right.Basic;

namespace study.NewFolder1.Right
{
    public partial class Controls : UserControl
    {
        List<Control> controls = new List<Control>();

        Button button1 = new Button();

        public Controls()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Dock = DockStyle.Fill;
            label1.Text = this.Width + ", " + this.Height;
            this.SizeChanged += new EventHandler(ThisSizeChanged);

            controls.Add(TitlePanel);

            // 假装读取本地表格来生成
            List<string> list = new List<string>();
            for (int i = 0; i < 2; i++)
                list.Add(Convert.ToString(i + 1));
            for (int i = 0; i < list.Count; i++)
            {
                Title title = new Title(list[i]);
                title.Changed += new MyEventHandler(Title_Changed);
                controls.Add(title);
            }

            // 添加很多时挂起会更快（不触发界面更改重新绘图事件）
            this.flowLayoutPanel1.SuspendLayout();
            for(int i = 0; i < controls.Count; i++)
            {
                this.flowLayoutPanel1.Controls.Add(controls[i]);
            }
            this.flowLayoutPanel1.ResumeLayout();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            // 添加很多时挂起会更快（不触发界面更改重新绘图事件）
            this.flowLayoutPanel1.SuspendLayout();

            Title title = new Title("laoshijidaidaiwo");
            title.Width = this.Width - 25;
            title.Changed += new MyEventHandler(Title_Changed);
            controls.Add(title);
            this.flowLayoutPanel1.Controls.Add(title);

            this.flowLayoutPanel1.ResumeLayout();
        }
        void Title_Changed(object obj, string str)
        {
            int i = 0;
        }
        void ThisSizeChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].Width = this.Width - 25;
            }
            label1.Text = this.Width + ", " + this.Height;
        }
    }
}
