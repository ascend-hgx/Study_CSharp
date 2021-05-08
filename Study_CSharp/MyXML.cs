using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace study
{
    /*
            MyXML xml = new MyXML("test.xml", "Sheet1");
            List<string> list = new List<string>();
            if (0 < xml.ReadExcelRow(list, 1))
                MessageBox.Show("参数读取错误");
            int i = 0;
            Console.WriteLine("第一行参数如下");
            for (; i < list.Count - 1; i++)
                Console.Write(list[i] + ", ");
            Console.WriteLine(list[i]);

            Console.WriteLine("第一列参数如下");
            List<string> column = new List<string>();
            if (0 < xml.ReadExcelColumn(column, 1))
                MessageBox.Show("参数读取错误");
            i = 0;
            for (; i < column.Count - 1; i++)
                Console.Write(column[i] + ", ");
            Console.WriteLine(column[i]);
     */
    class MyXML
    {
        static bool debug = false;

        public List<List<string>> SheetList = new List<List<string>>();
        public MyXML(string fileName, string sheetName)
        {
            if (0 != ReadExcelAll(SheetList, fileName, sheetName))
                MessageBox.Show("表格加载失败");
        }
        // 读取一行
        public int ReadExcelRow(List<string> needList, int needRow)
        {
            needRow--;
            if (SheetList == null)
                return 1;

            if (needRow > SheetList.Count)
                return 2;

            for(int i = 0; i < SheetList[needRow].Count; i++)
            {
                needList.Add(SheetList[needRow][i]);
            }

            return 0;
        }
        // 读取一列
        public int ReadExcelColumn(List<string> needList, int needColumn)
        {
            if (SheetList == null)
                return 1;

            bool haveColumn = false;
            for(int i = 0; i < SheetList.Count; i++)
            {
                if (SheetList[i].Count > needColumn)
                    haveColumn = true;
            }

            if (!haveColumn)
                return 2;

            needColumn--;
            for(int i = 0; i < SheetList.Count; i++)
            {
                if (SheetList[i].Count <= needColumn)
                    needList.Add("");
                else
                    needList.Add(SheetList[i][needColumn]);
            }

            return 0;
        }
        /// <summary>
        /// 读取某行数据
        /// </summary>
        /// <param name="neadList">要返回的链表，默认已string格式保存</param>
        /// <param name="fileName">打开的文件名</param>
        /// <param name="sheetName">需要的表格名</param>
        /// <param name="needRow">第几行</param>
        /// <returns></returns>
        public static int ReadExcelRow(List<string> neadList, string fileName, string sheetName, int needRow)
        {
            needRow--;      // 内部默认从0开始计算，
            XElement element = null;
            try
            {
                element = XElement.Load(@fileName);
            }
            catch
            {
                MessageBox.Show("加载文件失败");
                return 3;
            }
            //IEnumerable<string> Rows = from item in element.Elements("Sheet1")
            //                           select (string)item.Attribute("Row");
            List<XElement> sheet = element.Elements().ToList();

            if (debug)
            {
                Console.WriteLine(sheet.Count());
                //IEnumerable<XElement> childData = from item in element.Elements()
                //                                  select item.Element("Worksheet");
                //Console.WriteLine(childData.Count());
                //foreach (string str in childData)
                //    Console.WriteLine(str);
                for(int i = 0; i < sheet.Count; i++)
                {
                    Console.WriteLine(sheet[i].Name + "\r\n");
                    string name = sheet[i].Name.ToString();
                    if (name.Contains("Worksheet"))
                    {
                        // 显示定义标识符名字
                        Console.WriteLine(name);
                        // 显示标识符里面属性设定
                        Console.WriteLine(sheet[i].FirstAttribute);         // 读取第一个标注属性
                        Console.WriteLine(sheet[i].LastAttribute);          // 读取最后一个个标注属性
                        Console.WriteLine(sheet[i].Attributes("Name"));     // bug，并不知道干嘛的
                        Console.WriteLine(GetValue(sheet[i].FirstAttribute.ToString(), "\"", "\""));
                    }
                }
            }
            
            bool haveNeedTable = false;
            for(int i = 0; i < sheet.Count; i++)
            {

                string name = sheet[i].Name.ToString();
                // 确定是表格内容（Sheet）
                if (name.Contains("Worksheet"))
                {
                    string str = GetValue(sheet[i].FirstAttribute.ToString(), "\"", "\"");
                    // 有需要的Sheet
                    if (sheetName == str)
                    {
                        haveNeedTable = true;
                        List<XElement> listInSheet = sheet[i].Elements().ToList();
                        List<XElement> Rows = listInSheet[0].Elements().ToList();
                        for(int m = 0; m < Rows.Count; m++)
                        {
                            string row = Rows[m].Name.ToString();
                            if (debug)
                                Console.WriteLine("确定这行是什么:" + row);
                            // 确定是表格内容（Sheet）
                            if (!row.Contains("Row"))
                            {
                                Rows.RemoveAt(m);
                                m--;
                            }
                        }

                        if (debug)
                        {
                            Console.WriteLine("读取到需要的Sheet了");
                            Console.WriteLine(sheet[i].Value);                              // 显示里面需要读取的值
                            Console.WriteLine("总的有：" + Rows.Count() + "行数据");
                            for (int j = 0; j < Rows.Count; j++)
                            {
                                Console.WriteLine("读取到的如下有如下一行：");
                                Console.WriteLine(Rows[j]);
                            }
                        }

                        // 如果读取的行数大于所有行数报错输出
                        if (needRow > Rows.Count)
                        {
                            if (debug)
                                Console.WriteLine("请求错误：Sheet中没有这么多行数据");
                            return 2;
                        }

                        List<XElement> need = Rows[needRow].Elements().ToList();
                        int value = 0;
                        for (int j = 0; j < need.Count; j++)
                        {
                            string neadName = need[j].Name.ToString();

                            string rowAtrb = Convert.ToString(need[j].FirstAttribute);
                            if (rowAtrb.Contains("ss:Index"))
                            {
                                string getStr = GetValue(Convert.ToString(need[j].FirstAttribute), "\"", "\"");
                                if (getStr != "" && getStr != null)
                                {
                                    for (int m = value; m < Convert.ToInt32(getStr) - 1; m++)
                                    {
                                        value++;
                                        neadList.Add("");
                                    }
                                }
                            }

                            value++;
                            neadList.Add(need[j].Value);
                            //// 确定是表格内容（Sheet）
                            //if (name.Contains("Worksheet"))
                            //    neadList.Add(need[j].Value);
                        }

                        if (debug)
                        {
                            Console.WriteLine("读取到需要的一行了");  
                            for (int j = 0; j < need.Count; j++)
                            {
                                Console.WriteLine("读取到的一行参数如下：");
                                Console.WriteLine(need[j].Value);
                            }
                        }
                    }
                }
            }
            if (!haveNeedTable) // 没有对应表格
                return 1;

            return 0;      // 返回0则表示正常读取到参数
        }
        // 从str中读取start开始到end结束的string并返回
        public static string GetValue(string str, string start, string end)
        {
            Regex rg = new Regex("(?<=(" + start + "))[.\\s\\S]*?(?=(" + end + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }
        // 读取表格中所有数据已表格返回
        public static int ReadExcelAll(List<List<string>> neadList, string fileName, string sheetName)
        {
            XElement element = null;
            bool haveNeedTable = false;
            try
            {
                element = XElement.Load(@fileName);
            }
            catch
            {
                MessageBox.Show("加载文件失败");
                return 3;
            }
            List<XElement> sheet = element.Elements().ToList();
            for (int i = 0; i < sheet.Count; i++)
            {

                string name = sheet[i].Name.ToString();
                // 确定是表格内容（Sheet）
                if (name.Contains("Worksheet"))
                {
                    string str = GetValue(sheet[i].FirstAttribute.ToString(), "\"", "\"");
                    // 有需要的Sheet
                    if (sheetName == str)
                    {
                        haveNeedTable = true;
                        List<XElement> listInSheet = sheet[i].Elements().ToList();
                        List<XElement> Rows = listInSheet[0].Elements().ToList();
                        for (int m = 0; m < Rows.Count; m++)
                        {
                            string row = Rows[m].Name.ToString();
                            if (debug)
                                Console.WriteLine("确定这行是什么:" + row);
                            // 确定是表格内容（Sheet），把不是的移出后再处理
                            if (!row.Contains("Row"))
                            {
                                Rows.RemoveAt(m);
                                m--;
                            }
                        }

                        for(int j = 0; j < Rows.Count; j++)
                        {
                            List<string> rowList = new List<string>();
                            List<XElement> row = Rows[j].Elements().ToList();
                            int value = 0;
                            for(int m = 0; m < row.Count; m++)
                            {
                                // Contains是否包含
                                string rowAtrb = Convert.ToString(row[m].FirstAttribute);
                                if (rowAtrb.Contains("ss:Index"))
                                {
                                    string getStr = GetValue(Convert.ToString(row[m].FirstAttribute), "\"", "\"");
                                    if (getStr != "" && getStr != null)
                                    {
                                        for (int n = value; n < Convert.ToInt32(getStr) - 1; n++)
                                        {
                                            value++;
                                            rowList.Add("");
                                        }
                                    }
                                }
                                value++;
                                rowList.Add(row[m].Value);
                                if (debug)
                                    Console.WriteLine("添加" + j + "的一个变量为: " + row[m].Value);
                            }
                            if(rowList != null)
                                if(rowList.Count > 0)
                                    neadList.Add(rowList);
                        }
                    }
                }
            }
            if (!haveNeedTable) // 没有对应表格
                return 1;
            return 0;
        }

        public void Test()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("test.xml"); 
            XmlNodeList nodeList = xmlDocument.DocumentElement["Worksheet"].ChildNodes;

            // 列表中一部分
            Console.WriteLine(nodeList[0].InnerText);       // 对应一个表格中的所有数据
            //Console.WriteLine(nodeList[0].InnerXml);        // 对应每个表格中所有内容
            Console.WriteLine();
            //for(int i = 0; i < nodeList.Count; i++)
            //{
            //    XmlAttributeCollection attrs = nodeList[i].Attributes;  // 属性，这里读取到里面包含的所有数据合并在了一起
            //    Console.WriteLine(attrs[0].Value);                      // 显示
            //}

            XmlNode Row = nodeList.Item(0);     // 和下面直接读取一样，都是得到所有内容
            //XmlNode Row = nodeList[0];
            //Display the contents of the child nodes.
            if (Row.HasChildNodes)
            {
                for (int i = 0; i < Row.ChildNodes.Count; i++)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(Row.ChildNodes[i].InnerText);   // InnerText这里会显示没行中所有数据值
                    Console.WriteLine(Row.ChildNodes[i].InnerXml + "\r\n");
                }
            }


            XmlNode Cell = Row.ChildNodes[0];
            Console.WriteLine("Cell is : ");
            Console.WriteLine(Cell.HasChildNodes);
            Console.WriteLine(Cell.Name);
            Console.WriteLine(Cell.Value);
            Console.WriteLine(Cell.InnerXml);
            Console.WriteLine(Cell.InnerText); /* */
        }

        /// <summary>
        /// XML文件的读写，及数据流查询等基础操作
        /// </summary>
        public void Bases()
        {
            XNamespace aw = "http://www.adventure-works.com";
            XElement xmlTree1 = new XElement(aw + "first",
                new XElement(aw + "Child1", 1),
                new XElement(aw + "Child2", 2),
                new XElement(aw + "Child3", 3),
                new XElement(aw + "Child4", 4),
                new XElement(aw + "Child5", 5),
                new XElement(aw + "Child6", 6)
            );

            XElement xmlTree2 = new XElement(aw + "first",
                from el in xmlTree1.Elements()
                where ((int)el >= 3 && (int)el <= 5)
                select el
            );
            Console.WriteLine("xmlTree2：" + "\r\n" + xmlTree2 + "\r\n");


            XElement xmlTree3 = new XElement("Root",
                new XElement("Child1", 1),
                new XElement("Child2", 2),
                new XElement("Child3", 3),
                new XElement("Child4", 4),
                new XElement("Child5", 5),
                new XElement("Child6", 6)
            );

            // 使用SQL命令查询
            XElement xmlTree4 = new XElement("Root",
                from el in xmlTree3.Elements()
                where ((int)el >= 3 && (int)el <= 5)
                select el
            );
            Console.WriteLine("xmlTree4：" + "\r\n" + xmlTree4 + "\r\n");

            // 添加子集
            xmlTree4.Add(xmlTree2);
            xmlTree4.Add(new XElement("Add", new XElement("hello", 1)));

            Console.WriteLine("添加后的xmlTree4：" + "\r\n" + xmlTree4 + "\r\n");

            // 保存当前的XML到本地文件
            xmlTree4.Save(@"balabala.xml");

            // 加载本地的，也可以使用xmlTree5.Load(@"c:\myContactList.xml");
            XElement xmlTree5 = XElement.Load(@"balabala.xml");
            // 读取指定元素
            Console.WriteLine("xmlTree5：" + xmlTree5.Element("Child3") + "\r\n");
            // 内容数据显示
            foreach (int i in xmlTree5.Elements())
                Console.WriteLine("xmlTree5: " + i);

            IEnumerable<string> partNos = from item in xmlTree5.Descendants("first")
                                          select (string)item.Attribute("PartNumber");

            XElement xmlTree6 = new XElement("didi", from el in xmlTree5.Descendants("first")
                                                     select el.Element("Child3")
                                                     );
            Console.WriteLine("xmlTree6: " + xmlTree6);
        }
    }
}
