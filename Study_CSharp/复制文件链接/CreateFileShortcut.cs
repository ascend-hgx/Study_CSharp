using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        public void Read(string fileName)
        {
            fileDateList = myFile.Read(fileName);
            for (int i = 0; i < fileDateList.Count(); i++)
            {
                Console.WriteLine(fileDateList[i]);
            }
        }
    }
}
