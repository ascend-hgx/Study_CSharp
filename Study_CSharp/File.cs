using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;                        // 读取文件

namespace PublicFunctionLib
{
    /// <summary>
    /// 用枚举类型定义，代表执行那种命令
    /// </summary>
    public class Com
    {
        public string End = "End";              //用于保存每个起始位
        public string Error = "Error";          //文件读取错误
        public string Line = "Line";            //直线运动
        public string Circular = "Circular";    //圆弧运动
        public string Ellipse = "Ellipse";      //椭圆运动
    };

    /// <summary>
    /// 用来暂存每次读取到的控制命令，定义为struct不能有自己的链表
    /// </summary>
    public class MyCmd : Com
    {
        // 执行那种命令
        public string cmd;
        // 参数链表
        public IListDs<double> parameter = new LinkList<double>();
        // 走直线时确实定绝对运动还是相当运动
        public bool absolute = true;
    }
    public interface IMyFileStream
    {
        string startCom();  // 直接用string回报错
        void change(int num, IListDs<string> newList, string fileName = "");
        MyCmd readOneRow(int num, int thisRow);
        string readOneRow(int num, int thisRow, string fileName = "");
        int haveStartCommand(string file = "", string needStr = "");
        void write(IListDs<string> files, bool changed = false, string fileName = "");
        void write(string files, bool changed = false, string fileName = "");
        IListDs<string> read(string fileName = "");
        List<MyCmd> read(int num);
    }

    public class MyFileStream : MyFile, IMyFileStream
    {
        public MyFileStream()
        {
            read();
            haveStartCommand();
        }

        public string startCom()
        {
            return StartCom;
        }
        /// <summary>
        /// 用来该表某个启动链表对应的所有命令
        /// </summary>
        /// <param name="num"></param>
        /// <param name="fileName"></param>
        public void change(int num, IListDs<string> newList, string fileName = "")
        {
            // 默认我打开这个类自己定义的
            if (fileName.Length < 1) fileName = FileName;
            string filePath = Directory.GetCurrentDirectory() + "\\" + fileName + ".txt";

            // 读取当前文件中的所有链表
            IListDs<string> fileList = new LinkList<string>();
            fileList = read(fileName);

            // 将当前执行链表转换成string
            StringBuilder strBd = new StringBuilder();
            for (int i = 1; i < newList.Length; i++)
            {
                strBd.Append(newList.GetElem(i) + "\r\n");
            }
            if (newList.Length > 0)
                strBd.Append(newList.GetElem(newList.Length));
            string str = Convert.ToString(strBd);

            // 替换该行
            int startNum = 0;
            int j;
            for (int i = 1; i <= fileList.Length; i++)
            {
                // 计算到第几个启动命令
                string sr = fileList.GetElem(i);
                if (haveString(sr))
                {
                    startNum++;
                }

                // 如果等于该行，到要替换的地方
                if (startNum == num)
                {
                    j = i + 1;
                    // 一直删除，知道下一个启动命令。这里必须先判断j在合理范围以内在读取确定是否havestring
                    while (j <= fileList.Length && !haveString(fileList.GetElem(j)))
                    {
                        fileList.Delete(j);
                    }

                    // 添加新的对应命令
                    if (str.Length > 0)
                        fileList.InsertPost(str, i);
                    break;
                }
            }

            // 重写文件
            write(fileList, true);
        }
        public List<MyCmd> read(int num)
        {
            List<MyCmd> list = new List<MyCmd>();
            MyCmd myCmd = new MyCmd();
            bool i = true;
            int j = 1;

            while (i)
            {
                myCmd = readOneRow(num, j);
                j++;
                // 执行到下一行会读取到Error结束
                if (myCmd.cmd != myCmd.Error && myCmd.cmd != myCmd.End)
                {
                    list.Add(myCmd);
                }
                else
                {
                    i = false;
                }
            }

            return list;
        }
        /// <summary>
        /// 打开的文件为对象自定义的.
        /// 以下只是用于测试
        /// MyCmd myCmd = new MyCmd();
        /// myCmd = myFileStream.readOneRow(1, 1);
        /// </summary>
        /// <param name="num"></param>
        /// <param name="thisRow"></param>
        /// <returns></returns>
        public MyCmd readOneRow(int num, int thisRow)
        {
            int currentRow = 0;

            // 用来暂存每次读取到的控制命令
            MyCmd myCmd = new MyCmd();

            // 默认我打开这个类自己定义的
            string fileName = FileName;

            IListDs<string> file = new LinkList<string>();
            string cmd;
            int findNum = 0;

            //file = list; //read(fileName); // 读取文件中全部命令（默认使用初始读取的，不用每次都读取）
            file = read(fileName);

            for (int i = 1; i <= file.GetLength(); i++)
            {
                cmd = file.GetElem(i);   // 读取对应行元素
                bool haveStart = haveString(cmd, StartCom);

                // 读取起始点，到需要的第几个执行命令
                if (haveStart)
                {
                    findNum++;
                }
                // 正常读取对应起始命令以内的执行参数
                if (findNum == num && !haveStart)        // 到达第几个命令集，并且没有结束
                {
                    currentRow++;

                    if (currentRow == thisRow)
                    {
                        // Line( 1, 2, 2, 2, True )
                        int j = 0;
                        StringBuilder str = new StringBuilder();
                        while (cmd[j] != '(' && j < cmd.Length - 1)
                        {
                            str.Append(cmd[j++]);
                        }
                        myCmd.cmd = Convert.ToString(str);
                        while (j < cmd.Length - 1)
                        {
                            j++;
                            str.Clear();
                            while (cmd[j] != ',' && cmd[j] != ')')
                            {
                                str.Append(cmd[j++]);
                            }
                            // 如果读取的能转换成数字
                            if (canConvertToNum(str))
                            {
                                // 无法直接将StringBuilder转换成double
                                string parameter = Convert.ToString(str);
                                myCmd.parameter.Append(Convert.ToDouble(parameter));
                            }
                            else
                            {
                                if (haveString(str, "True"))
                                {
                                    myCmd.absolute = true;
                                }
                                else
                                    myCmd.absolute = false;
                            }
                        }

                        return myCmd;
                        //return file.GetElem(i);
                    }
                }

                // 如果已经到这个命令切有起始命令（而不是执行命令）则默认到了下一个起始命令
                if (findNum > num)
                {
                    myCmd.cmd = myCmd.End;
                    return myCmd;
                }
            }

            myCmd.cmd = "Error";
            return myCmd;
        }
        /// <summary>
        /// 判断是否可以转换成数字
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool canConvertToNum(StringBuilder cmd)
        {
            for (int i = 0; i < cmd.Length; i++)
            {
                if (cmd[i] == ' ')
                    cmd.Remove(i, 1);
            }

            double j = 0;
            string str = Convert.ToString(cmd);
            bool result = double.TryParse(str, out j); //i now = 108 

            return result;
        }

        /// <summary>
        /// 从文件名为对应起始命令的第一行开始，读取下面的第几行文件
        /// </summary>
        /// <param name="第几个命令组"></param>
        /// <param name="对应组中的第几行"></param>
        /// <returns></returns>
        /// public IListDs<string> readOnRow(string startName, int thisRow, string fileName = "")
        public string readOneRow(int num, int thisRow, string fileName = "")
        {
            int currentRow = 0;

            // 用来暂存每次读取到的控制命令
            MyCmd myCmd = new MyCmd();

            // 默认我打开这个类自己定义的
            if (fileName.Length < 1) fileName = FileName;

            IListDs<string> file = new LinkList<string>();
            string cmd;
            int findNum = 0;

            file = list;//read(fileName); // 读取文件中全部命令

            for (int i = 1; i <= file.GetLength(); i++)
            {
                cmd = file.GetElem(i);   // 读取对应行元素
                bool haveStart = haveString(cmd, StartCom);

                if (haveStart)
                {
                    findNum++;
                }
                if (findNum == num && !haveStart)        // 到达第几个命令集，并且没有结束
                {
                    if (thisRow == 0)
                        return file.GetElem(i);
                    if (currentRow < thisRow)
                        currentRow++;
                    else
                    {
                        return file.GetElem(i);
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// 读取输入的整个字符串中启动命令个数
        /// </summary>
        /// <param name="totalStr"></param>
        /// <param name="needStr"></param>
        /// <returns></returns>
        public int haveStartCommand(string file = "", string needStr = "")
        {
            if (file.Length < 1)
                file = syncRead();
            if (needStr.Length < 1)
                needStr = StartCom;

            int num = 0;

            list.Clear();
            if (file.Length > 0)
            {
                StringBuilder strBdr = new StringBuilder();

                for (int i = 0; i < file.Length - 1; i++)
                {
                    // 每到结尾则添加
                    if (file[i] != '\r' && file[i + 1] != '\n')
                    {
                        strBdr.Append(file[i]);
                    }
                    else
                    {
                        list.Append(Convert.ToString(strBdr));
                        strBdr.Clear();
                        i++;
                    }
                }
                if (strBdr.Length > 0)
                {
                    int i = file.Length - 1;
                    strBdr.Append(file[i]);
                    list.Append(Convert.ToString(strBdr));
                    strBdr.Clear();
                }
            }

            for (int i = 1; i <= list.Length; i++)
            {
                if (haveString(list.GetElem(i), needStr))
                {
                    num++;
                }
            }

            return num;
        }

    }

    public class MyFile
    {
        bool Debug = true;
        // protected 继承的对象可以调用，但外部不能访问
        protected IListDs<string> list = new LinkList<string>();
        protected string FileName = "MyFile";
        public string StartCom = "StartCommand: ";

        //// 静态变量不用声明就可以访问
        //public static string didi = "didi";
        //public static string getDidi()
        //{
        //    return didi;
        //}

        public MyFile() { }
        public MyFile(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// 同步写入文件：文件内容（会删除以前的再写），文件名
        /// </summary>
        /// <param name="files"></param>
        /// <param name="fileName"></param>
        public void write(IListDs<string> files, bool changed = false, string fileName = "")
        {
            // 如果没输入文件名，则文件名文初始化文件名
            if (fileName == "") { fileName = FileName; }
            string filePath = Directory.GetCurrentDirectory() + "\\" + fileName + ".txt";

            // 默认将文件分行
            StringBuilder file = new StringBuilder();
            for (int i = 1; i <= files.Length; i++)
            {
                file.Append(files.GetElem(i) + "\r\n");
            }

            try
            {
                FileStream fs = null;
                // 打开文件，如果没有则创建
                if (changed)
                    fs = new FileStream(filePath, FileMode.Create);
                else
                    fs = new FileStream(filePath, FileMode.Append);

                // 同步写入文件
                // Write the string array to a new file named "WriteLines.txt".
                //using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
                using (StreamWriter outputFile = new StreamWriter(fs))
                {
                    outputFile.Write(file); // WriteLine写入后自动换行

                    //清空缓冲区
                    outputFile.Flush();
                    //关闭流
                    outputFile.Close();
                    fs.Close();
                }
            }
            catch { }
        }
        /// <summary>
        /// 同步写入文件：文件内容（会删除以前的再写），文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="files"></param>
        public void write(string files, bool changed = false, string fileName = "")
        {
            // 如果没输入文件名，则文件名文初始化文件名
            if (fileName == "") { fileName = FileName; }

            try
            {
                // 打开文件，如果没有则创建
                string filePath = Directory.GetCurrentDirectory() + "\\" + fileName + ".txt";
                FileStream fs = null;
                // 打开文件，如果没有则创建
                if (changed)
                    fs = new FileStream(filePath, FileMode.Create);
                else
                    fs = new FileStream(filePath, FileMode.Append);

                // 同步写入文件
                // Write the string array to a new file named "WriteLines.txt".
                //using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
                using (StreamWriter outputFile = new StreamWriter(fs))
                {
                    if (files.Length > 0)
                        outputFile.WriteLine(files);    // WriteLine写入后自动换行

                    //清空缓冲区
                    outputFile.Flush();
                    //关闭流
                    outputFile.Close();
                    fs.Close();
                }
            }
            catch { }
        }

        /// <summary>
        /// 异步写入文件：文件内容（不清空，在后面添加文件，并且默认在最后添加\r\n以换行），文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="files"></param>
        public async void asyncWrite(string files, string fileName = "")
        {
            // 如果没输入文件名，则文件名文初始化文件名
            if (fileName == "") { fileName = FileName; }

            files += "\r\n";

            try
            {
                // 打开文件，如果没有则创建
                string filePath = Directory.GetCurrentDirectory() + "\\" + fileName + ".txt";

                FileStream fs = new FileStream(filePath, FileMode.Append);

                // Append text to an existing file named "WriteLines.txt".
                // 新建文件写入流，path将两句合成一句
                using (StreamWriter outputFile = new StreamWriter(filePath, true))
                {
                    await outputFile.WriteAsync(files);     // 异步写入文件，使用await异步需要在函数前面加async
                }
            }
            catch { }
        }
        /// <summary>
        /// 异步写入文件流，并在每行添加\r\n
        /// </summary>
        /// <param name="files"></param>
        /// <param name="fileName"></param>
        public async void asyncWrite(IListDs<string> files, string fileName = "")
        {
            // 如果没输入文件名，则文件名文初始化文件名
            if (fileName == "") { fileName = FileName; }
            string filePath = Directory.GetCurrentDirectory() + "\\" + fileName + ".txt";

            // 默认将文件分行
            StringBuilder file = new StringBuilder();
            for (int i = 1; i <= files.Length; i++)
            {
                file.Append(files.GetElem(i) + "\r\n");
            }

            try
            {
                // 打开文件，如果没有则创建
                FileStream fs = new FileStream(filePath, FileMode.Append);

                // 同步写入文件
                // Write the string array to a new file named "WriteLines.txt".
                //using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
                using (StreamWriter outputFile = new StreamWriter(fs))
                {
                    await outputFile.WriteAsync(file.ToString());     // 异步写入文件，使用await异步需要在函数前面加async
                }
            }
            catch { }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string syncRead(string fileName = "", bool isTxt = true)
        {
            // 如果没输入文件名，则文件名文初始化文件名
            if (fileName == "") { fileName = FileName; }

            if(isTxt)
                fileName += ".txt";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return "";      // 异常，什么都不返回
            }
        }

        /// <summary>
        /// 读取文件后，将每行分开，返回链表，并暂存链表到：list
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public IListDs<string> read(string fileName = "")
        {
            list.Clear();
            // 如果没输入文件名，则文件名文初始化文件名
            if (fileName == "") { fileName = FileName; }

            string file = syncRead(fileName);

            if (file.Length > 0)
            {
                StringBuilder strBdr = new StringBuilder();

                for (int i = 0; i < file.Length - 1; i++)
                {
                    // 每到结尾则添加
                    if (file[i] != '\r' && file[i + 1] != '\n')
                    {
                        strBdr.Append(file[i]);
                    }
                    else
                    {
                        list.Append(Convert.ToString(strBdr));
                        strBdr.Clear();
                        i++;
                    }
                }
                if (strBdr.Length > 0)
                {
                    int i = file.Length - 1;
                    strBdr.Append(file[i]);
                    list.Append(Convert.ToString(strBdr));
                    strBdr.Clear();
                }
            }

            return list;
        }
        public List<string> Read(string fileName = "", string address = "")
        {
            List<string> thisList = new List<string>();
            // 如果没输入文件名，则文件名文初始化文件名
            if (fileName == "") { fileName = FileName; }

            StringBuilder strBuider = new StringBuilder();
            strBuider.Append(address);
            strBuider.Append(fileName);
            string file = syncRead(strBuider.ToString(),false);

            if (file.Length > 0)
            {
                StringBuilder strBdr = new StringBuilder();

                for (int i = 0; i < file.Length - 1; i++)
                {
                    // 每到结尾则添加
                    if (file[i] != '\r' && file[i + 1] != '\n')
                    {
                        strBdr.Append(file[i]);
                    }
                    else
                    {
                        thisList.Add(Convert.ToString(strBdr));
                        strBdr.Clear();
                        i++;
                    }
                }
                if (strBdr.Length > 0)
                {
                    int i = file.Length - 1;
                    strBdr.Append(file[i]);
                    thisList.Add(Convert.ToString(strBdr));
                    strBdr.Clear();
                }
            }

            return thisList;
        }
        /* 读取显示当前目录有哪些文件（需要用到下面的changeStr和haveString）
         * File类创建、删除、读写文件
         * Directory类创建、删除、读写文件夹
         * FileInfo和File类相同，但不是静态类，DirectoryInfo和Directory也是
         * FileStream类，数据流用于打开文件并进行读写
         * 实例：
            static String[] strList = null;
            strList = read_file_content("", ".csv");
            for (int i = 0; i < strList.Length; i++)
            {
                Console.WriteLine(strList[i]);
            }
         */
        /// <summary>
        /// 读取显示当前目录有哪些文件（如txt类的文件，亦或找一个文件）
        /// </summary>
        /// <param name="address"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] read_file_content(string address, string name)
        {
            StringBuilder fullName = null;
            List<string> listStr = new List<string>();
            string[] str = null;
            string[] backStr = null;

            // FileInfo读取文件内容
            FileInfo fileInfo = new FileInfo(".");
            Console.WriteLine("打开文件目录：" + fileInfo.DirectoryName);      // DirectoryName当前文件目录
            Console.WriteLine("给定的文件名：" + fileInfo.Name);               // Name在构造函数中设定的文件名

            fullName = changeStr(fileInfo.FullName);        // string str = @"C:\\windows";赋值时字符串前加@才能保存所有符号，没有@则只有一个\
            Console.WriteLine(fullName);                    // FullName当前目录完全名称

            // Directory读取父、子目录状态
            //Console.WriteLine("Directory为静态方法，不能实例化");
            try
            {
                address = Convert.ToString(changeStr(address));
                str = Directory.GetDirectories(Convert.ToString(fullName));// 获取当前目录下有哪些文件夹
                Console.WriteLine("当前文件夹中有" + str.Length + "个子文件夹，分别为：");
                for (int i = 0; i < str.Length; i++)
                {
                    Console.WriteLine(str[i]);
                }

                // 如果没输入地址，已当前命令地址打开文件
                if (address.Length <= 0)
                    address = Convert.ToString(fullName);

                str = Directory.GetFiles(address);
                Console.WriteLine("当前文件夹中有" + str.Length + "个文件，分别是：");
                for (int i = 0; i < str.Length; i++)
                {
                    Console.WriteLine(str[i]);

                    if (name.Length > 0)
                    {
                        if (haveString(str[i], name))
                            listStr.Add(str[i]);
                    }
                    else
                        listStr.Add(str[i]);
                }
            }
            catch { }

            Console.WriteLine("文件查找完成");
            backStr = listStr.ToArray();
            return backStr;
        }
        // 在read_fale_content中用来转换文件地址
        private StringBuilder changeStr(string str)
        {
            StringBuilder backStr = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '\\')
                {
                    backStr.Append('/');
                }
                else
                {
                    backStr.Append(str[i]);
                }
            }

            return backStr;
        }
        /// <summary>
        /// 判断在总的string中是否有需要的
        /// </summary>
        /// <param name="str"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public bool haveString(string str, string compare = "")
        {
            int cmpNo = 0;

            if (compare == "")
                compare = StartCom;

            if (compare.Length > str.Length)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == compare[cmpNo])
                    cmpNo++;
                else
                    cmpNo = 0;

                if (cmpNo == compare.Length)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断在总的string中是否有需要的
        /// </summary>
        /// <param name="str"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public bool haveString(StringBuilder str, string compare = "")
        {
            int cmpNo = 0;

            if (compare == "")
                compare = StartCom;

            if (compare.Length > str.Length)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == compare[cmpNo])
                    cmpNo++;
                else
                    cmpNo = 0;

                if (cmpNo == compare.Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
