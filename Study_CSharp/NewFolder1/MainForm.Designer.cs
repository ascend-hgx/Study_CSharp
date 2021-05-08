namespace study.NewFolder1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Min = new System.Windows.Forms.PictureBox();
            this.Max = new System.Windows.Forms.PictureBox();
            this.Close = new System.Windows.Forms.PictureBox();
            this.TitlePicture = new System.Windows.Forms.PictureBox();
            this.Home = new System.Windows.Forms.Button();
            this.Project = new System.Windows.Forms.Button();
            this.Parameter = new System.Windows.Forms.Button();
            this.Config = new System.Windows.Forms.Button();
            this.Help = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 90);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Cyan;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.Size = new System.Drawing.Size(1061, 530);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 0;
            // 
            // Min
            // 
            this.Min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Min.Image = global::Study_CSharp.Port._563697;
            this.Min.InitialImage = global::Study_CSharp.Port.关闭按钮;
            this.Min.Location = new System.Drawing.Point(930, 12);
            this.Min.Name = "Min";
            this.Min.Size = new System.Drawing.Size(37, 36);
            this.Min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Min.TabIndex = 3;
            this.Min.TabStop = false;
            this.Min.Click += new System.EventHandler(this.Min_Click);
            // 
            // Max
            // 
            this.Max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Max.BackgroundImage = global::Study_CSharp.Port.关闭按钮;
            this.Max.Image = global::Study_CSharp.Port._11830671;
            this.Max.InitialImage = global::Study_CSharp.Port.关闭按钮;
            this.Max.Location = new System.Drawing.Point(973, 12);
            this.Max.Name = "Max";
            this.Max.Size = new System.Drawing.Size(37, 36);
            this.Max.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Max.TabIndex = 2;
            this.Max.TabStop = false;
            this.Max.Click += new System.EventHandler(this.Max_Click);
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.BackgroundImage = global::Study_CSharp.Port.关闭按钮;
            this.Close.Image = global::Study_CSharp.Port.关闭按钮;
            this.Close.InitialImage = global::Study_CSharp.Port.关闭按钮;
            this.Close.Location = new System.Drawing.Point(1016, 12);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(37, 36);
            this.Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Close.TabIndex = 1;
            this.Close.TabStop = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // TitlePicture
            // 
            this.TitlePicture.Image = ((System.Drawing.Image)(resources.GetObject("TitlePicture.Image")));
            this.TitlePicture.Location = new System.Drawing.Point(12, 12);
            this.TitlePicture.Name = "TitlePicture";
            this.TitlePicture.Size = new System.Drawing.Size(69, 51);
            this.TitlePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TitlePicture.TabIndex = 4;
            this.TitlePicture.TabStop = false;
            // 
            // Home
            // 
            this.Home.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Home.Location = new System.Drawing.Point(208, 46);
            this.Home.Name = "Home";
            this.Home.Size = new System.Drawing.Size(75, 30);
            this.Home.TabIndex = 5;
            this.Home.Text = "首页";
            this.Home.UseVisualStyleBackColor = true;
            this.Home.Click += new System.EventHandler(this.Home_Click);
            // 
            // Project
            // 
            this.Project.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Project.Location = new System.Drawing.Point(308, 46);
            this.Project.Name = "Project";
            this.Project.Size = new System.Drawing.Size(75, 30);
            this.Project.TabIndex = 6;
            this.Project.Text = "项目";
            this.Project.UseVisualStyleBackColor = true;
            this.Project.Click += new System.EventHandler(this.Project_Click);
            // 
            // Parameter
            // 
            this.Parameter.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Parameter.Location = new System.Drawing.Point(408, 46);
            this.Parameter.Name = "Parameter";
            this.Parameter.Size = new System.Drawing.Size(75, 30);
            this.Parameter.TabIndex = 7;
            this.Parameter.Text = "参数";
            this.Parameter.UseVisualStyleBackColor = true;
            this.Parameter.Click += new System.EventHandler(this.Parameter_Click);
            // 
            // Config
            // 
            this.Config.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Config.Location = new System.Drawing.Point(508, 46);
            this.Config.Name = "Config";
            this.Config.Size = new System.Drawing.Size(75, 30);
            this.Config.TabIndex = 8;
            this.Config.Text = "配置";
            this.Config.UseVisualStyleBackColor = true;
            this.Config.Click += new System.EventHandler(this.Config_Click);
            // 
            // Help
            // 
            this.Help.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Help.Location = new System.Drawing.Point(608, 46);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(75, 30);
            this.Help.TabIndex = 9;
            this.Help.Text = "帮助";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // Time
            // 
            this.Time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Time.Location = new System.Drawing.Point(42, 652);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(59, 19);
            this.Time.TabIndex = 10;
            this.Time.Text = "timer";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(87, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 27);
            this.label1.TabIndex = 11;
            this.label1.Text = "Dream";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1065, 690);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.Config);
            this.Controls.Add(this.Parameter);
            this.Controls.Add(this.Project);
            this.Controls.Add(this.Home);
            this.Controls.Add(this.TitlePicture);
            this.Controls.Add(this.Min);
            this.Controls.Add(this.Max);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitlePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox Close;
        private System.Windows.Forms.PictureBox Max;
        private System.Windows.Forms.PictureBox Min;
        private System.Windows.Forms.PictureBox TitlePicture;
        private System.Windows.Forms.Button Home;
        private System.Windows.Forms.Button Project;
        private System.Windows.Forms.Button Parameter;
        private System.Windows.Forms.Button Config;
        private System.Windows.Forms.Button Help;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Label label1;
    }
}