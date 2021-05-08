using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace study.NewFolder1
{
    class MyExcel
    {
        List<List<string>> AllData = new List<List<string>>();
        List<string> ColumnsName = new List<string>();

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
                    ColumnsName.Add(range.Value.ToString());
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
                    AllData.Add(list);
            }

            // 垃圾回收，不用这个无法释放Excel
            int generation = System.GC.GetGeneration(app);
            workbook.Close();
            app.Quit();
            System.GC.Collect(generation);
        }

        // 读取所有参数
        public List<List<string>> ReadAllData()
        {
            return AllData;
        }

        // 读取一行
        public bool ReadRow(int row, List<string> backList)
        {
            if (row > AllData.Count)
                return false;

            row--;
            backList.Clear();
            for(int i = 0; i < AllData[row].Count; i++)
            {
                backList.Add(AllData[row][i]);
            }
            return true;
        }

        // 读取一列
        public bool ReadColumn(int column, List<string> backList)
        {
            if (column > ColumnsName.Count)
                return false;

            column--;
            backList.Clear();
            for(int i = 0; i < AllData.Count; i++)
            {
                if (AllData[i][column] == null)
                    backList.Add("");
                else
                    backList.Add(AllData[i][column]);
            }
            return true;
        }

        public bool ReadColumn(string columnName, List<string> backList)
        {
            if (columnName == null)
                return false;

            int column = -1;
            for(int i = 0; i < ColumnsName.Count; i++)
            {
                if(columnName == ColumnsName[i])
                {
                    column = i;
                    break;
                }
            }
            if (column < 0)
                return false;

            backList.Clear();
            for(int i = 0; i < AllData.Count; i++)
            {
                if (AllData[i][column] == null)
                    backList.Add("");
                else
                    backList.Add(AllData[i][column]);
            }

            return true;
        }
    }
}
