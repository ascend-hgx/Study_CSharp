using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace study.NewFolder1.Right
{
    public partial class HomePage : UserControl
    {
        bool showPanel = true;
        List<Control> controls = new List<Control>();

        public HomePage()
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

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1_SizeChanged(null, null);

            controls.Add(TitlePanel);
            controls.Add(CommonUseData);
            controls.Add(panel1);
            controls.Add(button1);
            controls.Add(dataGridView1);
            controls.Add(button2);

            for(int i = 1; i < controls.Count; i += 2)
                controls[i].Click += new System.EventHandler(this.CommonUseData_Click);

        }
        void UpdatePage()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.Add(controls[0]);
            flowLayoutPanel1.Controls.Add(controls[1]);
            if(showPanel)
            {
                flowLayoutPanel1.Controls.Add(panel1);
                flowLayoutPanel1.Controls.Add(button1);
            }
            flowLayoutPanel1.Controls.Add(dataGridView1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Show();
        }
        void flowLayoutPanel1_SizeChanged(object sentor, System.EventArgs e)
        {
            int width = this.flowLayoutPanel1.Width - 22;
            for (int i = 0; i < controls.Count; i++)
                controls[i].Width = width;

            //this.flowLayoutPanel1.Refresh();                      // 刷新重新绘制
            //flowLayoutPanel1.Controls.Remove(dataGridView1);      // 移出
            // 清空重新添加显示
            this.Controls.Clear();
            this.Controls.Add(flowLayoutPanel1);
            this.Show();

            FormLength.Text = "";
            FormLength.Text += "子窗体长度：" + this.Width + "\r\n";
            FormLength.Text += "容器长度: " + this.flowLayoutPanel1.Width + "\r\n";
            FormLength.Text += "主页显示长度: " + this.TitlePanel.Width + "\r\n";
        }
        private void CommonUseData_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (sender.Equals(controls[i]))
                    MessageBox.Show("点击了" + sender.ToString());
            }
            if (showPanel)
                showPanel = false;
            else
                showPanel = true;

            UpdatePage();
        }
    }
}
