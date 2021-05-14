using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;        // 文件处理
using System.Diagnostics;      // Process,通过Process使用Windows自带命令

using PublicFunctionLib;

namespace Study_CSharp.复制文件链接
{
    class CreateFileShortCut
    {
        bool debug = true;
        MyFile myFile = new MyFile();
        List<string> fileDateList = new List<string>();
        public void Test()
        {
            CopyDirectory(@"D:/code", "./");
            List<string> dirList = GetDirFileList(".txt");
            for(int i = 0; i < dirList.Count; i++)
            {
                DeleteFileList(dirList[i]);
            }
            for(int i = 0; i < dirList.Count; i++)
            {
                CopyFileList(dirList[i]);
                // 执行Windows命令
                ExitCmd("help");
                // 复制文件
                // 删除拷贝后文件
                DeleteFileList(dirList[i]);
            }
        }
        // 执行cmd命令
        public void ExitCmd(string cmd)
        {
            string strInput = cmd;
            Process p = new Process();
            // 设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            // 是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接收来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            // 输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            // 启动程序
            p.Start();
            // 向cmd发送信息
            p.StandardInput.WriteLine(strInput + "&exit");
            p.StandardInput.AutoFlush = true;
            // 获取输出信息
            string strOutput = p.StandardOutput.ReadToEnd();
            // 等等程序执行完退出进程
            p.WaitForExit();
            p.Close();
            Console.WriteLine(strOutput);
        }
        // 文件夹下所有内容copy
        public bool CopyDirectory(string sourcePath, string destinationPath = "", bool overWriteexisting = false)
        {
            bool ret = false;
            try
            {
                sourcePath = sourcePath.EndsWith(@"\") ? sourcePath : sourcePath + @"\";
                destinationPath = destinationPath.EndsWith(@"\") ? destinationPath : destinationPath + @"\";
                if (Directory.Exists(sourcePath))
                {
                    if (Directory.Exists(destinationPath) == false)
                        Directory.CreateDirectory(destinationPath);
                    foreach(string fls in Directory.GetFiles(sourcePath))
                    {
                        FileInfo fileInfo = new FileInfo(fls);
                        fileInfo.CopyTo(destinationPath + fileInfo.Name, overWriteexisting);
                    }
                    foreach(string drs in Directory.GetDirectories(sourcePath))
                    {
                        DirectoryInfo dirInfo= new DirectoryInfo(drs);
                        if (CopyDirectory(drs, destinationPath + dirInfo.Name, overWriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch(Exception ex)
            {
                ret = false;
            }
            return ret;
        }
        public void CopyFileList(string fileName = "文件列表.txt", string dir = @"D:/code/")
        {
            Read(fileName);
            if (debug)
                Console.WriteLine("已读取文件中每行数据并保持到fileDataList");
            CreateListDirectory(fileDateList);
            if (debug)
                Console.WriteLine("根据list中每行去除文件夹中文件名后，创建文件夹完成");
            CopyListFile(fileDateList, dir);
        }
        public List<string> GetDirFileList(string fileType, string dir = @".\")
        {
            List<string> list = new List<string>();
            string path = dir;
            if (dir == @".\")
                path = Directory.GetCurrentDirectory(); // 获取当前文件夹路径
            // 获取当前文件夹下又哪些文件
            DirectoryInfo root = new DirectoryInfo(path);
            string[] fileList = Directory.GetFiles(path, "*" + fileType);
            for(int i = 0; i < fileList.Length; i++)
            {
                list.Add(fileList[i]);
            }
            return list;
        }
        // 到dir文件夹目录，用递归的方式把所有fileType文件类型都删除
        public void DeleteTypeFile(string fileType, string dir = @".\")
        {
            string path = dir;
            if (dir == @".\")
                path = Directory.GetCurrentDirectory();     // 获取当前文件夹路径
            // 获取当前文件夹下又哪些文件
            DirectoryInfo root = new DirectoryInfo(path);
            // 获取文件夹对应类型的文件，并进行删除
            string[] deleteFile = Directory.GetFiles(path, "*" + fileType);
            for(int i = 0; i < deleteFile.Length; i++)
            {
                if (debug)
                    Console.WriteLine(deleteFile[i]);
                try
                {
                    File.Delete(deleteFile[i]);
                }
                catch
                {
                    Console.WriteLine("Delete error : " + deleteFile[i]);
                }
            }
            // 获取当前文件夹下有哪些文件夹
            DirectoryInfo[] dics = root.GetDirectories();
            for(int i = 0; i < dics.Length; i++)
            {
                if (debug)
                {
                    Console.WriteLine(dics[i].Name);
                    Console.WriteLine(dics[i].FullName);
                }
                DeleteTypeFile(fileType, dics[i].FullName);
            }
        }
        public void DeleteFileList(string fileName = "文件列表.txt", string dir = @".\")
        {
            if (debug)
                Console.WriteLine("读取文件中每行数据并保持到fileDateList");
            Read(fileName);
            // 读取对应整体文件路径
            for(int i = 0; i < fileDateList.Count; i++)
            {
                try
                {
                    File.Delete(dir + fileDateList[i]);
                }
                catch
                {
                    Console.WriteLine("Delete error : " + fileDateList[i]);
                }
            }
            if (debug)
                Console.WriteLine("完成文件删除");
        }
        public void CopyFile(string file, string dirct = "")
        {
            string fileName = GetFileNameFromStr(file);
            try
            {
                File.Copy(file, dirct + fileName, true);    // overWrite设为true可以覆盖
            }
            catch
            {
                Console.WriteLine("Copy error : " + file);
            }
        }
        public void CopyListFile(List<string> list, string sourceDir = "", string destDir = "")
        {
            string soureceFileName = "";
            string destFileName = "";
            for(int i = 0; i < list.Count; i++)
            {
                soureceFileName = sourceDir + list[i];
                destFileName = destDir + list[i];
                CopyFile(soureceFileName, destFileName);
            }
        }
        public void CreateListDirectory(List<string> directoryList)
        {
            for(int i = 0; i < directoryList.Count; i++)
            {
                if(directoryList[i] != "" && directoryList[i] != "\r\n")
                {
                    string dirct = GetDirectoryFromStr(directoryList[i]);
                    CreateDirectory(dirct);
                }
            }
        }
        public List<string> Read(string fileName)
        {
            fileDateList = myFile.Read(fileName);
            if(debug)
                for (int i = 0; i < fileDateList.Count(); i++)
                {
                    Console.WriteLine(fileDateList[i]);
                }
            return fileDateList;
        }
        public string GetDirectoryFromStr(string file)
        {
            string[] listStr = file.Split('\\');
            if (listStr.Length < 2)
                listStr = file.Split('/');
            StringBuilder strBdr = new StringBuilder();
            for(int i = 0; i < listStr.Length - 1; i++)
            {
                if (i < listStr.Length - 2)
                    strBdr.Append(listStr[i] + '\\');
                else
                    strBdr.Append(listStr[i]);
            }
            return Convert.ToString(strBdr);
        }
        public string GetFileNameFromStr(string file)
        {
            string[] listStr = file.Split('\\');
            if (listStr.Length < 2)
                listStr = file.Split('/');
            return listStr[listStr.Length - 1];
        }
        public void CreateDirectory(string dirct)
        {
            if (!Directory.Exists(dirct))
            {
                Directory.CreateDirectory(dirct);
            }
        }
    }
}
