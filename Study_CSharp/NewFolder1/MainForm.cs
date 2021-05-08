using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;                    // 用于系统定时刷新
using study.NewFolder1.Left;
using WindowsFormsApplication9;
using study.NewFolder1.Right;

namespace study.NewFolder1
{
    public partial class MainForm : Form
    {
        LeftBasis leftBasis = new LeftBasis();
        MyTreeView treeView = new MyTreeView();

        HomePage homePage = new HomePage();
        Controls controls = new Controls();

        public MainForm()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            TimerInit(10);  // 用作界面显示时间

            this.BackColor = Color.FromArgb(140, 188, 188);

            leftBasis.TreeViewHaveSelect += new TreeView_AfterSelect(ShowRight);
        }
        void ShowRight(object obj, string s)
        {
            List<string> list = new List<string>();
            list = obj as List<string>;

            switch (list[0])
            {
                case "Parent":
                    ShowRight(homePage);
                    break;
                case "右边窗体大小":
                    MessageBox.Show(this.splitContainer1.Panel2.Width.ToString() + ", " + this.splitContainer1.Panel2.Height.ToString());
                    break;
                case "右边灵活添加并执行对应事件":
                    ShowRight(controls);
                    break;
            }
        }
        void TimerInit(int time)
        {
            //系统定时器
            System.Timers.Timer timer;
            //高刷新定时器初始化
            timer = new System.Timers.Timer();
            timer.Interval = time;              // 单位：毫秒
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
            //timer.Stop();
        }
        /* 定时器Timer触发事件
         */
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Invoke(new EventHandler(delegate
            {
                Time.Text = DateTime.Now.ToString();
            }));
        }

        void ShowLeft(Control control)
        {
            this.splitContainer1.Panel1.Controls.Clear();
            this.splitContainer1.Panel1.Controls.Add(control);
            this.splitContainer1.Panel1.Update();
            control.Show();
        }
        void ShowRight(Control control)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            this.splitContainer1.Panel2.Controls.Add(control);
            this.splitContainer1.Panel2.Update();
            control.Show();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            ShowLeft(leftBasis);
        }

        private void Project_Click(object sender, EventArgs e)
        {
            ShowRight(leftBasis);
        }

        private void Parameter_Click(object sender, EventArgs e)
        {

        }

        private void Config_Click(object sender, EventArgs e)
        {

        }

        private void Help_Click(object sender, EventArgs e)
        {

        }

        //FormBorderStyle.None时，支持改变窗体大小
        #region 支持自适应大小及全屏、关闭按钮
        private const int Guying_HTLEFT = 10;
        private const int Guying_HTRIGHT = 11;
        private const int Guying_HTTOP = 12;
        private const int Guying_HTTOPLEFT = 13;
        private const int Guying_HTTOPRIGHT = 14;
        private const int Guying_HTBOTTOM = 15;
        private const int Guying_HTBOTTOMLEFT = 0x10;
        private const int Guying_HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else
                            m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else
                            m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标
                    m.LParam = IntPtr.Zero; //默认值
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }


        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        bool isMax = false;
        private void Max_Click(object sender, EventArgs e)
        {
            if (!isMax)
            {
                isMax = true;
                this.Max.Image = Study_CSharp.Port._1183068;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                isMax = false;
                this.Max.Image = Study_CSharp.Port._11830671;
                this.WindowState = FormWindowState.Normal;
            }
        }
        #endregion
    }
}
