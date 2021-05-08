using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;        // 文件处理

using PublicFunctionLib;

namespace Study_CSharp.复制文件链接
{
    class CreateFileShortcut
    {
        MyFile myFile = new MyFile();
        List<string> fileDateList = new List<string>();
        public void Test()
        {
            Read("fileList.txt");
            string derect = GetDirectory(fileDateList[1]);
            Console.WriteLine(derect);
            string fileName = GetFileName(fileDateList[1]);
            Console.WriteLine(fileName);
            CreateDirectory("NewDir");
        }
        public List<string> Read(string fileName)
        {
            fileDateList = myFile.Read(fileName);
            for (int i = 0; i < fileDateList.Count(); i++)
            {
                Console.WriteLine(fileDateList[i]);
            }
            return fileDateList;
        }
        public string GetDirectory(string file)
        {
            string[] listStr = file.Split('\\');
            StringBuilder strBdr = new StringBuilder();
            for (int i = 0; i < listStr.Length - 1; i++)
            {
                // Console.WriteLine(listStr[i]);
                if(i < listStr.Length - 2)
                    strBdr.Append(listStr[i] + '\\');
                else
                    strBdr.Append(listStr[i]);
            }

            return strBdr.ToString();
        }
        public string GetFileName(string file)
        {
            string[] listStr = file.Split('\\');
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
