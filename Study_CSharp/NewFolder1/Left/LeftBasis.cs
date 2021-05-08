using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PublicFunctionLib;
using study;

namespace study.NewFolder1.Left
{
    public delegate void TreeView_AfterSelect(object sender, string e);

    public partial class LeftBasis : UserControl
    {
        public LeftBasis()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top 
                                                                | System.Windows.Forms.AnchorStyles.Bottom)
                                                                | System.Windows.Forms.AnchorStyles.Left)
                                                                | System.Windows.Forms.AnchorStyles.Right)));
            this.Dock = DockStyle.Fill;         // 跟随父类调整大小

            treeView1.BeginUpdate();
            treeView1.Nodes.Add("Parent");
            int num = treeView1.Nodes.Count - 1;
            treeView1.Nodes[num].Nodes.Add("child");
            treeView1.Nodes[num].Nodes[0].Nodes.Add("greadt");
            treeView1.Nodes[num].Nodes.Add("child");
            treeView1.Nodes[num].Nodes[0].Nodes.Add("greadt");
            treeView1.Nodes[num].Nodes.Add("child");
            treeView1.Nodes[num].Nodes[0].Nodes.Add("greadt");
            treeView1.Nodes[num].Nodes.Add("child");
            treeView1.Nodes[num].Nodes[0].Nodes.Add("greadt");
            treeView1.Nodes[num].Nodes.Add("child");
            treeView1.Nodes[num].Nodes[0].Nodes.Add("greadt");
            treeView1.Nodes[num].Nodes.Add("child");
            treeView1.Nodes[num].Nodes[0].Nodes.Add("greadt");
            treeView1.Nodes[num].Nodes.Add("child");
            treeView1.Nodes[num].Nodes[0].Nodes.Add("greadt");
            treeView1.Nodes.Add("右边灵活添加并执行对应事件");
            treeView1.Nodes.Add("右边窗体大小");
        }
        public event TreeView_AfterSelect TreeViewHaveSelect;
        // e.Node.Text 点前点击的名字
        public void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<string> list = new List<string>();
            TreeNode tree = e.Node;
            Console.WriteLine(tree.FullPath);           // 显示当前路径
            while (tree != null)
            {
                list.Add(tree.Text);
                tree = tree.Parent;
            }
            list.Reverse();     // 反转

            TreeViewHaveSelect(list, "LeftBasis");

            string str = "";
            for (int i = 0; i < list.Count; i++)
                str += list[i] + ">>";
            //MessageBox.Show(str);
        }
    }
}
