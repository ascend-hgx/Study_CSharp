using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;

namespace PublicFunctionLib
{
    public partial class Port : Form
    {
        static bool _continue;
        static SerialPort _serialPort = new SerialPort();
        StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
        Thread readThread = new Thread(Read);

        public Port()
        {
            InitializeComponent();
            InitParameter();

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.Open();                         // 打开端口
            _continue = true;
            readThread.Start();

            //Console.WriteLine("Type QUIT to exit");
            //try
            //{
            //    string message = _serialPort.ReadLine();
            //    _serialPort.WriteLine("Hello world");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //readThread.Join();
            //_serialPort.Close();
        }

        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    Console.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }

        // 有输入，可以用这个的回车发送
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // 发送数据
        private void send_Click(object sender, EventArgs e)
        {
            // 读取输入的字符
            string str = textBox1.Text;
            Console.WriteLine(str);
        }

        // 接收数据
        private void receive_Click(object sender, EventArgs e)
        {

        }

        private void port_m_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object myPort;
                myPort = port_m.SelectedItem;
                _serialPort.PortName = Convert.ToString(myPort);

                object mySpeed;
                mySpeed = speed.SelectedItem;
                _serialPort.BaudRate = Convert.ToInt32(mySpeed);

                object myParity;
                myParity = odd_even.SelectedItem;
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(myParity), true);

                object myData;
                myData = data_num.DataSource;
                _serialPort.DataBits = Convert.ToInt32(myData);

                object myStop;
                myStop = stop_num.SelectedItem;
                _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(myStop), true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
