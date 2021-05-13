using System;
using System.Collections.Generic;       // 包含List<T>对象
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicFunctionLib;

using System.Collections;               // ArrayList使用需要添加
using System.Threading;                 // 多线程
using System.IO;                        // 读取文件
using System.IO.Ports;                  // 用于端口com通讯
using System.Timers;                    // 用于系统定时刷新

using System.ComponentModel.Composition;    // 串口使用

using study.NewFolder1; 

// 以下的SQL或者其他数据库操作
// SQL Server对应System.Data.SqlClient
// ODBC对应System.Data.Odbc
// OLEDB对应System.Data.OleDb
// Oracle对应System.Data.OracleClient
using System.Data;
using System.Data.SqlClient;            // 使用SQL Server数据库
using System.Data.OleDb;

using System.Xml.Linq;                  // 使用XElement操作XML文件

using System.Windows.Forms;             // 用来显示Form窗体

using System.Runtime.InteropServices;   // 用来设定数据类型
using System.Collections.Specialized;
using Study_CSharp.复制文件链接;

namespace study
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //GetSocket socket = new GetSocket();
            //socket.test();

            MyExcel myExcel = new MyExcel();
            List<string> list = new List<string>();
            list.Add("hello");
            list.Add("world");
            //myExcel.WriteRow(2, list, )

            Console.Read();
        }
        // string添加、减少、取值等
        static void StrSplit()
        {
            string str = "i love you";
            string[] listStr = str.Split(' ');
            foreach (string s in listStr)
                Console.WriteLine(s);
            Console.WriteLine(str.IndexOf("vy"));
            if (str.Contains("lo"))
                Console.WriteLine("lo");
            IList<string> list = new List<string>();
            list.Add("i");
            list.Add("love");
            list.Add("you");
            Console.WriteLine(list[2]);
            Console.WriteLine(list.Count);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("i");
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.ToString();
            list.RemoveAt(1);
        }

        // 以下三行，结合delegate与事件使用，实现类似于回调功能
        static void DelegateAndEvent()
        {
            SubFunc subFunc = new SubFunc();
            subFunc.myevent += new MyEventHandler(SubFuncs_Handler);
            subFunc.TestTriggerEvent();
        }
        static void SubFuncs_Handler(object sender, string e)
        {
            MessageBox.Show("子类中产生了事件，触发我在Main中执行");
            Console.WriteLine(sender + " " + e);
        }
        public delegate void MyEventHandler(object sender, string e);
        class SubFunc
        {
            public event MyEventHandler myevent;
            public void TestTriggerEvent()
            {
                myevent("你好哦", "test");
            }
        }

        /* 递归算法
         * T(n) = 7                 n == 1
         * T(n) = 2T(n/2) + 5n²     n  > 1
         */
        static int recursion(int i)
        {
            if (i == 1)
                return 7;
            else
                return 2 * recursion(i / 2) + 5 * i * i;
        }

        // 使用前按照下面第一行注释，建立数据库后再使用
        static void sqlTest()
        {
            // 需要现在“视图”》“服务器资源管理器”中添加本地数据库，对应数据库属性中“连接字符串”复制给connect string
            string connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\21994\Documents\test.mdf;Integrated Security=True;Connect Timeout=30";
            try
            {
                SqlConnection sqlCnt = new SqlConnection(connectString);
                sqlCnt.Open();

                // 以下可以直接使用这个函数：SqlCommand command = sqlCnt.CreateCommand(); 
                SqlCommand command = new SqlCommand();
                command.Connection = sqlCnt;            // 绑定SqlConnection对象
                command.CommandType = CommandType.Text;

                //// 执行SQL命令（也可以这样重复使用）
                //command = new SqlCommand("CREATE TABLE 表格1(姓名 int IDENTITY(1, 1) PRIMARY KEY not null,cname char(50))", sqlCnt);
                //command.ExecuteNonQuery();

                // 设定控制命令，并执行。在new的写入命令也可以，但目前这个如果已经有这个表格再添加会停止这
                command.CommandText = "CREATE TABLE 表格2(text int IDENTITY(1, 1) PRIMARY KEY not null,cname char(50))";
                command.ExecuteNonQuery();

                command.Dispose();
            }

            catch { }
        }
        struct MyStruct {public  int a;public int b; }
        static MyStruct structTest()
        {
            MyStruct myStruct;
            myStruct.a = 1;
            myStruct.b = 2;
            return myStruct;
        }
        // 运行Form1窗体
        static void runForm()
        {
            // Application只能运行一个Form作为主窗体
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        static void Com()
        {
            SerialPort serialPort = new SerialPort();

            serialPort.PortName = "COM1";       // 使用第几个COM口，可以SerialPort.GetPortNames()读取
            serialPort.BaudRate = 230400;       // 通讯速度
            //serialPort.Parity =               // 奇偶校验
            //serialPort.DataBits               // 每个字节数据位长度
            //serialPort.StopBits               // 停止位数
            //serialPort.Handshake              // 握手协议

            serialPort.Write("abcd");
            byte[] buffer = null;
            int 偏移量 = 0;
            int 读取个数 = 100;
            serialPort.Read(buffer, 偏移量, 读取个数);
        }

        /// <summary>
        /// 测试直接使用SQL
        /// </summary>
        static void SQL()
        {
            // Specify the data source.
            int[] scores = new int[] { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            foreach (int i in scoreQuery)
            {
                Console.Write(i + " ");
            }
        }
        // 通过Next(x, y)返回从X到Y范围内的随机数，这个返回0到1的随机浮点数
        static double 随机数() {
            Random random = new Random();
            return random.NextDouble();     
        }
        static void 与或非()
        {
            uint uiValue = ~(uint)Math.Pow(2, (3 - 1));
            uint And = 0xFFFFFFFF;
            uiValue = And | uiValue;
            Console.WriteLine(uiValue);
        }
        
        public static void 委托()
        {
            // Instantiate the delegate.实例化委托
            Del handler = DelegateMethod;
            // Call the delegate.执行委托
            handler("Hello World");
            var obj = new MyFileStream();
        }

        /* 示例：
         * // Instantiate the delegate.实例化委托
            Del handler = DelegateMethod;
            // Call the delegate.执行委托
            handler("Hello World");
         */
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="message"></param>
        public delegate void Del(string message);
        // Create a method for a delegate.
        public static void DelegateMethod(string message)
        {
            Console.WriteLine(message);
        }

        // 计时器
        public void timerWatch()
        {
            //计时器
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();      // 开始计时

            t_foreach();

            watch.Stop();       //停止计时

            //watch.ElapsedMilliseconds，返回int类型，计时时间
            string time = Convert.ToString(watch.ElapsedMilliseconds);
            Console.WriteLine(time);
        }

        // type parameter T in angle brackets
        /// <summary>
        /// 泛型测试
        /// GenericList<int> genericTest = new GenericList<int>();
        /// genericTest.test();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class GenericList<T>
        {
            // The nested class is also generic on T.
            private class Node
            {
                // T used in non-generic constructor.
                public Node(T t)
                {
                    next = null;
                    data = t;
                }

                private Node next;
                public Node Next
                {
                    get { return next; }
                    set { next = value; }
                }

                // T as private member data type.
                private T data;

                // T as return type of property.
                public T Data
                {
                    get { return data; }
                    set { data = value; }
                }
            }

            private Node head;

            // constructor
            public GenericList()
            {
                head = null;
            }

            // T as method parameter type:
            public void AddHead(T t)
            {
                Node n = new Node(t);
                n.Next = head;
                head = n;
            }

            /// <summary>
            /// 跌倒获取泛型中变量并输出
            /// </summary>
            /// <returns></returns>
            public IEnumerator<T> GetEnumerator()
            {
                Node current = head;

                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }

            public void test()
            {
                // int is the type argument
                GenericList<int> list = new GenericList<int>();

                for (int x = 0; x < 10; x++)
                {
                    list.AddHead(x);
                }

                foreach (int i in list)
                {
                    System.Console.Write(i + " ");
                }
                System.Console.WriteLine("\nDone");
            }
        }

        /* 如果原本就是string返回值为null也没什么影响
         * 但更改stringBuilder值为null返回时转换成string则会发生错误
         * string是基础变量可以设为null，StringBuilder是对象，需要实例化StringBuilder test = new StringBuilder();
         */
        static string returnNull()
        {
            StringBuilder test = new StringBuilder();
            return test.ToString();
        }

        static void TimerInit(int time)
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
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            t_switch();
        }

        /* 结构是一种值类型，通常用来封装一组相关的变量
         * 结构的构造函数必须带参数
         * 结构可以实现接口，不能从另一个结构或类继承
         * 示例代码：
         *  t_struct rect;
         *  rect.width = 2;
         *  rect.height = 3;
         *  Console.WriteLine(rect.Aread());
         */
        struct t_struct
        {
            public double width;
            public double height;
            public void Rect(double x, double y)
            {
                width = x;
                height = y;
            }
            public double Aread()
            {
                return width * height;
            }
        }

        // foreach用于枚举一个集合的元素，不应用于更改该集合内容，已避免产生不可避免的错误
        static void t_foreach()
        {
            ArrayList alt = new ArrayList();    // 可以动态添加和删除元素，元素需要更改推荐使用这种
            alt.Add("hello");
            alt.Add("wawu");
            foreach(string name in alt)
            {
                Console.WriteLine(name);
            }

            int[] arr = new int[2] { 1, 2 };
            int[] arrAdd = new int[arr.Length + 1]; // 当前没有使用，添加数组元素
            // 结合数组
            foreach(int number in arr)
            {
                Console.WriteLine(number);
            }
        }

        // switch中如果有语句的都必须加break，需要执行多个只能添加goto跳转执行
        static void t_switch()
        {
            int i = 0;

            switch (i)
            {
                case 0:
                    // 如果这里有语句就必须有break命令结束，没有则调到下一步继续执行
                case 1:
                    Console.WriteLine("value is 1");
                    goto case2;
                case 2:
                    case2:          // 上面的goto跳转到这里
                    Console.WriteLine("value is 2");
                    break;
                default:
                    break;
            }
        }

        // 链表使用
        static void t_list()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(3);
            Console.WriteLine("list总的个数：" + list.Count);
            foreach(int i in list)
            {
                Console.WriteLine(i);
            }

            list.Clear();
            Console.WriteLine("list总的个数：" + list.Count);
        }

        /* 多线程
         * Thread.Sleep(time) 线程延时时间
         * Start启动线程；Suspend挂起；Resume取消挂起；Abort终止运行；Join运行线程结束前阻塞其他线程；Sleep暂停
         * Priority线程优先级；互锁（比如互锁打印任务）三种方法（lock、Monitor、Mutex）
         */
        static void t_thread()
        {
            Thread thread1 = new Thread(new ThreadStart(Com));
            Thread thread2 = new Thread(new ThreadStart(t_switch));
            thread1.Priority = ThreadPriority.Lowest;   // 设定为低优先级运行
            thread2.Priority = ThreadPriority.Highest;  // 设定为高优先级运行
            Thread.Sleep(100);     // 线程停止运行
            
            thread1.Start();

            // 判断线程是否正在运行
            if(thread1.ThreadState == ThreadState.Running)
            {
                //当前这两个指令被废弃不建议使用了，并且不要把其他语句放到这个语句之前，以避免判断还在运行，但调用挂起时已经停止运行
                thread1.Suspend();      // 挂起线程
                thread1.Resume();       // 继续执行已挂起的线程

                Console.WriteLine("thread1调用的t_list正在运行，下面挂起后继续执行，但可能被Abort中止只执行一部分");
            }
            
            thread1.Abort("终止此线程");
            
            thread1.Join();      // 阻塞调用线程，直到运行中的线程终止为止

            thread2.Start();

            Thread.Sleep(100);
        }

        /* 多线程
         * run立即执行，start会计算合适后再执行（start、run为异步，异步执行的完全和其他线程无法）
         * RunSynchronously为同步执行
         * Join阻塞主线程和下面的Wait比较类似，但这个可以继续执行
         * CancelAfter(1000)一秒后取消任务
         * Wait方法会等待当前线程执行完成，再调用其他线程
         * WaitAll等待这些任务全部完成，相反WaitAny等待任意一个完成
         * IsCompleted获取任务是否已完成
         */
        static void task()
        {
            // Action委托
            Action<object> action = (object obj) =>
            {
                for(int i = 0; i< 10; i++) {
                    Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                        Task.CurrentId, obj,
                        Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(100);
                }
            };

            // Create a task but do not start it. 创建线程但不执行
            Task t1 = new Task(action, "alpha");

            // Construct a started task。创建并执行线程
            Task t2 = Task.Factory.StartNew(action, "beta");
            // Block the main thread to demonstrate that t2 is executing
            t2.Wait();

            // Launch t1，停止的t1开始执行
            t1.Start();
            Console.WriteLine("t1 开始执行. (Main Thread={0})",
                          Thread.CurrentThread.ManagedThreadId);
            // Wait for the task to finish.等待t1完成
            t1.Wait();

            // Construct a started task using Task.Run.用run的方法创建一个运行线程
            String taskData = "delta";
            Task t3 = Task.Run(() => {
                Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                                  Task.CurrentId, taskData,
                                   Thread.CurrentThread.ManagedThreadId);
            });
            // Wait for the task to finish.等待执行完成
            t3.Wait();
            //t3.IsCompleted;   // 获取任务是否已完成
            // Construct an unstarted task，创建不启动线程
            Task t4 = new Task(action, "gamma");
            // Run it synchronously，同步运行该线程
            t4.RunSynchronously();
            // Although the task was run synchronously, it is a good practice
            // to wait for it in the event exceptions were thrown by the task.
            t4.Wait();

            Task t5 = new Task(action, "gamma");
            Task t6 = new Task(action, "gamma");
            Task t7 = new Task(action, "gamma");
            Task t8 = new Task(action, "gamma");
            Task t9 = new Task(action, "gamma");
            t5.Start();
            t6.Start();
            t7.Start();
            monitor();      // 测试Monitor是否为单线程锁定
            t8.Start();
            t9.Start();
            
            // Wait for all tasks to complete.其中WaitAll等待这些任务全部完成
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Task.Run(() => {
                    Console.WriteLine("WaitAll id={0}，thread={1}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(1000);
                    });
            }
            try
            {
                Task.WaitAll(tasks);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("One or more exceptions occurred: ");
                foreach (var ex in ae.Flatten().InnerExceptions)
                    Console.WriteLine("   {0}", ex.Message);
            }
        }

        /* monitor实现锁定，但我并没有测试成功
         * monitor运行时其他线程也能运行
         * monitor与lock本质一样，锁定（多线程只有一个能执行该操作），不能实例化
         * 需要使用enter开始，exit结束
         */
        static void monitor()
        {
            int m = 0;
            // Action委托
            Action<object> action = (object obj1) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                        Task.CurrentId, obj1,
                        Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(100);
                    m++;
                }
            };
            Task t1 = new Task(action, "test1");
            Task t2 = new Task(action, "test2");

            var obj = new Object();

            t2.Start();
            Monitor.Enter(action);
            try
            {
                m++;
                t1.Start();
                for(int i =0; i < 10; i++) { Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"); }
            }
            finally
            {
                Monitor.Exit(action);
            }
        }
        
    }

    /* get读取当前变量，set设置当前变量
     * Add add = new Add(1,2);
     * Console.WriteLine(add.result);
     */ 
    public class Add
    {
        int a = 0, b = 0;
        public int A       // 使用时，给A赋值并将变量保存到a中
        {
            set
            {
                a = value;  
            }
        }
        public int B
        {
            set
            {
                b = value;
            }
        }

        public int result
        {
            get
            {
                return a + b;
            }
        }

        public Add(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }


    public struct CutTableStruct
    {
        [MarshalAs(UnmanagedType.I2)]
        public Int16 iNo;
        [MarshalAs(UnmanagedType.I2)]
        public Int16 iCutNum;
        [MarshalAs(UnmanagedType.R8)]
        public Double lrTotalLen;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public Double[] ar_lrCutLen;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string sName;
    }

}
