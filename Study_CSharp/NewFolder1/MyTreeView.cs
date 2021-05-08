using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApplication9
{
    public partial class MyTreeView : TreeView
    {
        public Color TreeBackColor { get; set; } = Color.FromArgb(240, 255, 255);
        public Color TreeHoverColor { get; set; } = Color.FromArgb(200, 200, 255);
        public Color TreeSelectColor { get; set; } = Color.FromArgb(222, 222, 255);
        public Color TreeTextColor { get; set; } = Color.FromArgb(0, 0, 0);


        public MyTreeView()
        {
            InitializeComponent();
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            this.FullRowSelect = true;
            this.ItemHeight = 30;
            this.HotTracking = true;
            this.ShowLines = true;
            this.CheckBoxes = false;
            this.Scrollable = true;
            this.BackColor = TreeBackColor;
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            //节点背景绘制
            if (e.Node.IsSelected)
            {
                e.Graphics.FillRectangle(new SolidBrush(TreeSelectColor), e.Bounds);
            }
            else if ((e.State & TreeNodeStates.Hot) != 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(TreeHoverColor), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(TreeBackColor), e.Bounds);
                Bitmap bitmap = new Bitmap(global::study.Properties.Resources._20150502183904);
                e.Graphics.DrawImage(bitmap, e.Bounds);
            }

            //节点头图标绘制
            if (e.Node.IsExpanded)
            {
                e.Graphics.DrawImage(global::study.Properties.Resources.tree_NodeExpend, e.Node.Bounds.X - 16, e.Node.Bounds.Y + 6);
            }
            else if (e.Node.IsExpanded == false && e.Node.Nodes.Count > 0)          // 有CheckBoxs的话这里是26
            {
                e.Graphics.DrawImage(global::study.Properties.Resources.tree_NodeCollaps, e.Node.Bounds.X - 16, e.Node.Bounds.Y + 6);
            }

            //文本绘制
            if (!e.Bounds.IsEmpty)
            {
                Font foreFont = new Font("微软雅黑", 10);
                Brush drawText = new SolidBrush(TreeTextColor);
                e.Graphics.DrawString(e.Node.Text, foreFont, drawText, e.Node.Bounds.Left + 0, e.Node.Bounds.Top + 5);
            }

            //图标绘制
            //e.Graphics.DrawIcon(new Icon(@"C:\Users\wangrui\Desktop\MyTreeView\WindowsFormsApplication9\SQK_Ui\Resources\map.ico", 16, 16), e.Node.Bounds.X + 8, e.Node.Bounds.Y + 2);

            if (this.CheckBoxes == true)
            {
                //绘制复选框
                if (e.Node.Checked == true)
                {
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Node.Bounds.X - 10, e.Node.Bounds.Y + 5), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
                }
                else if (e.Node.Checked == false)
                {
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Node.Bounds.X - 10, e.Node.Bounds.Y + 5), System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            TreeNode tn = this.GetNodeAt(e.Location);
            this.SelectedNode = tn;
        }

        TreeNode currentNode = null;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            TreeNode tn = this.GetNodeAt(e.Location);
            Graphics g = this.CreateGraphics();
            if (currentNode != tn)
            {
                //绘制当前节点的hover背景
                if (tn != null)
                    OnDrawNode(new DrawTreeNodeEventArgs(g, tn, new Rectangle(0, tn.Bounds.Y, this.Width, tn.Bounds.Height), TreeNodeStates.Hot));

                //取消之前hover的节点背景
                if (currentNode != null)
                    OnDrawNode(new DrawTreeNodeEventArgs(g, currentNode, new Rectangle(0, currentNode.Bounds.Y, this.Width, currentNode.Bounds.Height), TreeNodeStates.Default));
            }
            currentNode = tn;
            g.Dispose();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //移出控件时取消Hover背景
            if (currentNode != null)
            {
                Graphics g = this.CreateGraphics();
                OnDrawNode(new DrawTreeNodeEventArgs(g, currentNode, new Rectangle(0, currentNode.Bounds.Y, this.Width, currentNode.Bounds.Height), TreeNodeStates.Default));
            }
        }

        #region 组件设计器生成的代码
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseTreeView
            // 
            this.ResumeLayout(false);

        }

        #endregion
    }
}
