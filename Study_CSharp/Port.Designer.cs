using System.IO.Ports;
using System.Text;
using System.Collections.Generic;
using System;

namespace PublicFunctionLib
{
    partial class Port
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.port_m = new System.Windows.Forms.ComboBox();
            this.speed = new System.Windows.Forms.ComboBox();
            this.odd_even = new System.Windows.Forms.ComboBox();
            this.data_num = new System.Windows.Forms.ComboBox();
            this.stop_num = new System.Windows.Forms.ComboBox();
            this.send = new System.Windows.Forms.Button();
            this.receive = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "端口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(173, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "通讯速度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(471, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "数据位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(322, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "奇偶校验";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(620, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位";
            // 
            // port_m
            // 
            this.port_m.FormattingEnabled = true;
            this.port_m.Location = new System.Drawing.Point(24, 99);
            this.port_m.Name = "port_m";
            this.port_m.Size = new System.Drawing.Size(121, 23);
            this.port_m.TabIndex = 9;
            // 
            // speed
            // 
            this.speed.FormattingEnabled = true;
            this.speed.Location = new System.Drawing.Point(173, 99);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(121, 23);
            this.speed.TabIndex = 10;
            // 
            // odd_even
            // 
            this.odd_even.FormattingEnabled = true;
            this.odd_even.Location = new System.Drawing.Point(322, 99);
            this.odd_even.Name = "odd_even";
            this.odd_even.Size = new System.Drawing.Size(121, 23);
            this.odd_even.TabIndex = 11;
            // 
            // data_num
            // 
            this.data_num.FormattingEnabled = true;
            this.data_num.Location = new System.Drawing.Point(471, 99);
            this.data_num.Name = "data_num";
            this.data_num.Size = new System.Drawing.Size(121, 23);
            this.data_num.TabIndex = 12;
            // 
            // stop_num
            // 
            this.stop_num.FormattingEnabled = true;
            this.stop_num.Location = new System.Drawing.Point(620, 99);
            this.stop_num.Name = "stop_num";
            this.stop_num.Size = new System.Drawing.Size(121, 23);
            this.stop_num.TabIndex = 13;
            // 
            // send
            // 
            this.send.Font = new System.Drawing.Font("宋体", 12F);
            this.send.Location = new System.Drawing.Point(792, 43);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(100, 30);
            this.send.TabIndex = 14;
            this.send.Text = "发送";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // receive
            // 
            this.receive.Font = new System.Drawing.Font("宋体", 12F);
            this.receive.Location = new System.Drawing.Point(792, 91);
            this.receive.Name = "receive";
            this.receive.Size = new System.Drawing.Size(100, 32);
            this.receive.TabIndex = 15;
            this.receive.Text = "接收";
            this.receive.UseVisualStyleBackColor = true;
            this.receive.Click += new System.EventHandler(this.receive_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(24, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 157);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 24);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(856, 127);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Location = new System.Drawing.Point(24, 359);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(868, 157);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 23);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(856, 127);
            this.textBox2.TabIndex = 1;
            // 
            // Port
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.receive);
            this.Controls.Add(this.send);
            this.Controls.Add(this.stop_num);
            this.Controls.Add(this.data_num);
            this.Controls.Add(this.odd_even);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.port_m);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Port";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitParameter()
        {
            //comboBox1.Items.Add(textBox1.Text);

            // 查询更新已有端口
            IList<string> list = new List<string>();
            // 读取当前有哪些COM口
            foreach (string s in SerialPort.GetPortNames())
            {
                list.Add(s);
            }
            port_m.DataSource = list;

            // 传输速度
            int[] speed_data = new int[] { 9600, 19200, 38400, 115200 };
            speed.DataSource = speed_data;

            // 奇偶校验
            list.Clear();
            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                list.Add(s);
            }
            odd_even.DataSource = list;

            // 数据位
            int[] data_length = new int[] { 8 };
            data_num.DataSource = data_length;            // DataBits

            // 停止位
            list.Clear();
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                list.Add(s);
            }
            stop_num.DataSource = list;            // StopBits

            // 触发事件
            this.port_m.SelectedValueChanged += new System.EventHandler(this.port_m_SelectedIndexChanged);
        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox port_m;
        private System.Windows.Forms.ComboBox speed;
        private System.Windows.Forms.ComboBox odd_even;
        private System.Windows.Forms.ComboBox data_num;
        private System.Windows.Forms.ComboBox stop_num;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Button receive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox2;
    }
}