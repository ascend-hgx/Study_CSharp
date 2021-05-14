using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Data;

namespace study.NewFolder1
{
    class MyExcel
    {
        List<List<string>> allData = new List<List<string>>();
        List<string> columnsName = new List<string>();
        Excel.Workbook workbook = null;

        ~MyExcel()
        {
            if (allData != null)
                allData.Clear();
            if (columnsName != null)
                columnsName.Clear();
            if(workbook != null)
                workbook.Close();
        }
        public MyExcel() { }
        public MyExcel(string fileName)
        {
            Excel.Application app = new Excel.Application();
            string file = Directory.GetCurrentDirectory() + @"\" + fileName;

            if (app == null)
            {
                MessageBox.Show("Can't access excel");
                return;
            }

            try
            {
                workbook = app.Application.Workbooks.Open(file, null, false);
            }
            catch
            {
                MessageBox.Show("Failed to open the excel");
            }
        }
        public MyExcel(string fileName, string sheetName)
        {
            if (fileName == null || fileName == "")
                MessageBox.Show("Excel opening error");

            Excel.Application app = new Excel.Application();
            Excel.Workbook workbook = null;
            Excel._Worksheet sheet = null;

            if(app == null)
            {
                MessageBox.Show("Can't access excel");
                return;
            }

            try
            {
                workbook = app.Application.Workbooks.Open(fileName, null, true);
            }
            catch
            {
                MessageBox.Show("Failed to open the excel");
                return;
            }

            sheet = (Excel.Worksheet)workbook.Worksheets[sheetName];
            int rowsCount = sheet.UsedRange.Cells.Rows.Count;           // 得到行数
            int columnsCount = sheet.UsedRange.Cells.Columns.Count;     // 得到列数

            Excel.Range range = sheet.UsedRange;
            for(int i = 1; i <= columnsCount; i++)
            {
                range = sheet.Range[sheet.Cells[1,i], sheet.Cells[1, i]];
                if(range.Value != null && range.Value != "")
                {
                    columnsName.Add(range.Value.ToString());
                }
            }

            //byte[] bt = System.Text.Encoding.Default.GetBytes("A");
            //byte[] deviation = new byte[] { 0x65 };
            // data加载时每一行的数据最少也要加表头这么多
            for(int i = 2; i <= rowsCount; i++)
            {
                List<string> list = new List<string>();
                for(int j = 1; j <= columnsCount; j++)
                {
                    byte[] deviation = new byte[] { 0x65 };
                    deviation[0] = Convert.ToByte(64 + j);
                    string readPara = System.Text.Encoding.Default.GetString(deviation) + Convert.ToString(i);
                    
                    range = sheet.get_Range(readPara, System.Reflection.Missing.Value);
                    //range = sheet.Range[sheet.Cells[i, j], sheet.Cells[i, j]];
                    if(range.Value != null)
                    {
                        list.Add(range.Value.ToString());
                    }
                    else
                    {
                        list.Add("");
                    }
                }
                bool ok = false;
                for(int j = 0; j < columnsCount; j++)
                {
                    if(list[j] != null && list[j] != "")
                    {
                        ok = true;
                        break;
                    }
                }
                if (ok)
                    allData.Add(list);
            }

            // 垃圾回收，不用这个无法释放Excel
            int generation = System.GC.GetGeneration(app);
            workbook.Close();
            app.Quit();
            System.GC.Collect(generation);
        }

        // 写入第几行数据，返回是否写入成功
        public bool WriteRow(int row, List<string> writeList, Microsoft.Office.Interop.Excel.Worksheet workSheet = null)
        {
            if(workSheet == null)
            {
                return false;
            }
            for(int i = 0; i < writeList.Count; i++)
            {
                workSheet.Cells[i + 1][row] = writeList[i];
            }
            return true;
        }
        // 读取所有参数
        public List<List<string>> ReadallData()
        {
            return allData;
        }

        // 读取一行
        public bool ReadRow(int row, List<string> backList)
        {
            if (row > allData.Count)
                return false;

            row--;
            backList.Clear();
            for(int i = 0; i < allData[row].Count; i++)
            {
                backList.Add(allData[row][i]);
            }
            return true;
        }

        // 读取一列
        public bool ReadColumn(int column, List<string> backList)
        {
            if (column > columnsName.Count)
                return false;

            column--;
            backList.Clear();
            for(int i = 0; i < allData.Count; i++)
            {
                if (allData[i][column] == null)
                    backList.Add("");
                else
                    backList.Add(allData[i][column]);
            }
            return true;
        }

        public bool ReadColumn(string columnName, List<string> backList)
        {
            if (columnName == null)
                return false;

            int column = -1;
            for(int i = 0; i < columnsName.Count; i++)
            {
                if(columnName == columnsName[i])
                {
                    column = i;
                    break;
                }
            }
            if (column < 0)
                return false;

            backList.Clear();
            for(int i = 0; i < allData.Count; i++)
            {
                if (allData[i][column] == null)
                    backList.Add("");
                else
                    backList.Add(allData[i][column]);
            }

            return true;
        }

        public DataTable GetDataFromExcelByCom(bool hasTitle = false)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.Cancel)
                return null;
            var excelFilePath = openFile.FileName;

            Excel.Application app = new Excel.Application();
            Excel.Sheets sheets;
            object oMissiong = System.Reflection.Missing.Value;
            Excel.Workbook workbook = null;
            DataTable dt = new DataTable();

            try
            {
                if (app == null) return null;
                workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                    oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                sheets = workbook.Worksheets;

                //将数据读入到DataTable中
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);//读取第一张表  
                if (worksheet == null) return null;

                int iRowCount = worksheet.UsedRange.Rows.Count;
                int iColCount = worksheet.UsedRange.Columns.Count;
                //生成列头
                for (int i = 0; i < iColCount; i++)
                {
                    var name = "column" + i;
                    if (hasTitle)
                    {
                        var txt = ((Excel.Range)worksheet.Cells[1, i + 1]).Text.ToString();
                        if (!string.IsNullOrWhiteSpace(txt)) name = txt;
                    }
                    while (dt.Columns.Contains(name)) name = name + "_1";//重复行名称会报错。
                    dt.Columns.Add(new DataColumn(name, typeof(string)));
                }
                //生成行数据
                Excel.Range range;
                int rowIdx = hasTitle ? 2 : 1;
                for (int iRow = rowIdx; iRow <= iRowCount; iRow++)
                {
                    DataRow dr = dt.NewRow();
                    for (int iCol = 1; iCol <= iColCount; iCol++)
                    {
                        range = (Excel.Range)worksheet.Cells[iRow, iCol];
                        dr[iCol - 1] = (range.Value2 == null) ? "" : range.Text.ToString();
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch { return null; }
            finally
            {
                workbook.Close(false, oMissiong, oMissiong);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
                app.Workbooks.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
            }
        }
    }
}
