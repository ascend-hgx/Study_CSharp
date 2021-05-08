namespace study.NewFolder1.Right
{
    partial class HomePage
    {
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.TitlePicture = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.CommonUseData = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FormLength = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.TitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePicture)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanel1.Controls.Add(this.TitlePanel);
            this.flowLayoutPanel1.Controls.Add(this.CommonUseData);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.dataGridView1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(654, 456);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            // 
            // TitlePanel
            // 
            this.TitlePanel.AutoScroll = true;
            this.TitlePanel.Controls.Add(this.TitlePicture);
            this.TitlePanel.Controls.Add(this.Title);
            this.TitlePanel.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitlePanel.Location = new System.Drawing.Point(3, 3);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(636, 48);
            this.TitlePanel.TabIndex = 1;
            // 
            // TitlePicture
            // 
            this.TitlePicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitlePicture.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TitlePicture.Location = new System.Drawing.Point(3, 34);
            this.TitlePicture.Name = "TitlePicture";
            this.TitlePicture.Size = new System.Drawing.Size(600, 2);
            this.TitlePicture.TabIndex = 1;
            this.TitlePicture.TabStop = false;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Location = new System.Drawing.Point(12, 12);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(47, 19);
            this.Title.TabIndex = 0;
            this.Title.Text = "主页";
            // 
            // CommonUseData
            // 
            this.CommonUseData.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CommonUseData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommonUseData.FlatAppearance.BorderSize = 0;
            this.CommonUseData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommonUseData.Location = new System.Drawing.Point(3, 57);
            this.CommonUseData.Name = "CommonUseData";
            this.CommonUseData.Size = new System.Drawing.Size(636, 42);
            this.CommonUseData.TabIndex = 2;
            this.CommonUseData.Text = "常用参数";
            this.CommonUseData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CommonUseData.UseVisualStyleBackColor = false;
            //this.CommonUseData.Click += new System.EventHandler(this.CommonUseData_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FormLength);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 77);
            this.panel1.TabIndex = 3;
            // 
            // FormLength
            // 
            this.FormLength.AutoSize = true;
            this.FormLength.Location = new System.Drawing.Point(131, 17);
            this.FormLength.Name = "FormLength";
            this.FormLength.Size = new System.Drawing.Size(55, 15);
            this.FormLength.TabIndex = 1;
            this.FormLength.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "内部窗体大小：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(645, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(522, 296);
            this.dataGridView1.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(645, 305);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "HomePage";
            this.Size = new System.Drawing.Size(654, 456);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.TitlePanel.ResumeLayout(false);
            this.TitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.PictureBox TitlePicture;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button CommonUseData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label FormLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
    }
}
