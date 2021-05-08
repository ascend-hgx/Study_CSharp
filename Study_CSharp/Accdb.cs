using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;                // 保存accdb时使用   
using System.Data;                      // 同上，添加这个使用DataSet 

namespace PublicFunctionLib
{
    /* 使用accdb读写Access表
     * 目前read已经可以使用：accdb.Read("Card1", "default", "ID"); 
     */
    public class Accdb
    {
        //根据excle的路径把第一个sheel中的内容放入datatable
        public DataTable ReadExcelToTable(string path)//excel存放的路径
        {
            try
            {

                //连接字符串
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; // Office 07及以上版本 不能出现多余的空格 而且分号注意
                                                                                                                                                 //string connstring = Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"; //Office 07以下版本 
                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串
                                                                                       //string sql = string.Format("SELECT * FROM [{0}] WHERE [日期] is not null", firstSheetName); //查询字符串

                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);                  // 文件中添加或刷新行
                    return set.Tables[0];
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// 文件中写入值
        /// </summary>
        /// <param name="文件地址"></param>
        /// <param name="执行命令"></param>
        public void write(string connectionString, string queryString)
        {
            DataSet dataSet = new DataSet("Suppliers");

            // 连接文件
            using (OleDbConnection connection =
                        new OleDbConnection(connectionString))
            {
                // 创建适配器
                OleDbDataAdapter adapter =
                    new OleDbDataAdapter(queryString, connection);

                // Set the parameters.选择参数
                adapter.SelectCommand.Parameters.Add(
                    "@CategoryName", OleDbType.VarChar, 80).Value = "toasters";
                adapter.SelectCommand.Parameters.Add(
                    "@SerialNum", OleDbType.Integer).Value = 239;

                // Open the connection and fill the DataSet.打开并刷新文件
                try
                {
                    connection.Open();
                    adapter.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // The connection is automatically closed when the
                // code exits the using block.
            }
        }

        /// <summary>
        /// 对要开的文件使用SQL命令操作
        /// </summary>
        /// <param name="文件地址"></param>
        /// <param name="要执行的SQL命令"></param>
        public void InsertRow(string file = "Card1", string insertSQL = "")
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                            + file + ".accdb;Persist Security Info=False";

            // 自定义SQL用于测试
            insertSQL = "";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // The insertSQL string contains a SQL statement that
                // inserts a new row in the source table.
                OleDbCommand command = new OleDbCommand(insertSQL);

                // Set the Connection to the new OleDbConnection.
                command.Connection = connection;

                // Open the connection and execute the insert command.
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // The connection is automatically closed when the
                // code exits the using block.
            }
        }

        /// <summary>
        /// 读取文件，文件名、行名、文件名
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="sFormulaName"></param>
        /// <param name="sDataName"></param>
        public void Read(string sFileName = "Card1", string sFormulaName = "default", string sDataName = "ID")
        {
            // 文件打开名
            string conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                            + sFileName + ".accdb;Persist Security Info=False";
            // 搜索文件（SQL命令，类似网站中的MySQL）,DirectionInfo不要更改
            string sql = "select * from [DirectionInfo] where [name]='" + sFormulaName + "'";

            try
            {
                // 数据适配器，用于加载文件
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conStr);
                // 命令
                OleDbCommandBuilder buider = new OleDbCommandBuilder(adapter);
                // 数据内存中缓存
                DataSet dsMsg = new DataSet();
                // 适配器刷新文件给缓存
                adapter.Fill(dsMsg);
                // 获取数据缓存的起始地址，这个貌似可以不用
                string[] cc = new string[dsMsg.Tables[0].Rows.Count];
                
                if(sDataName != null)
                    Console.WriteLine(dsMsg.Tables[0].Rows[0][sDataName].ToString());   // 读取一行来显示
            }
            catch (Exception ex)
            {
                throw new Exception("DirectionInfo数据库读取失败" + ex.Message.ToString());
            }
        }

        public void write(string sFileName, string sFormulaName, string sDataName = null)
        {

        }
        /*******************************************************************************/
        /*   内部变量（为区分属性，在内部变量上增加In标志）                            */
        /*******************************************************************************/
        private double dEncodeDiameter = 60.2;
        private double dFactorX = 1.0;
        private double dFactorY = 1.0;
        private int    iEncoderMode = 2;
        private double dXInverse = 1.0;                       //X反向
        private double dYInverse = 1.0;                       //Y反向
        private double dFlyInverse = 1.0;                     //飞行编码器反向
        private double dAngleInverse = 1.0;                   //弧角顺逆时针反向
        public void DataRead(string sFileName, string sFormulaName)
        {
            string conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                            + sFileName + ".accdb;Persist Security Info=False";
            string sql = "select * from [DirectionInfo] where [name]='" + sFormulaName + "'";

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conStr);
                OleDbCommandBuilder buider = new OleDbCommandBuilder(adapter);
                DataSet dsMsg = new DataSet();
                adapter.Fill(dsMsg);
                string[] cc = new string[dsMsg.Tables[0].Rows.Count];

                dEncodeDiameter = Convert.ToDouble(dsMsg.Tables[0].Rows[0]["dEncodeDiameter"].ToString());
                dFactorX        = Convert.ToDouble(dsMsg.Tables[0].Rows[0]["dFactorX"].ToString());
                dFactorY        = Convert.ToDouble(dsMsg.Tables[0].Rows[0]["dFactorY"].ToString());
                iEncoderMode    = Convert.ToInt32(dsMsg.Tables[0].Rows[0]["iEncoderMode"].ToString());
                dXInverse       = Convert.ToDouble(dsMsg.Tables[0].Rows[0]["dXInverse"].ToString()); 
                 dYInverse      = Convert.ToDouble(dsMsg.Tables[0].Rows[0]["dYInverse"].ToString());       
                dFlyInverse     = Convert.ToDouble(dsMsg.Tables[0].Rows[0]["dFlyInverse"].ToString());
                dAngleInverse   = Convert.ToDouble(dsMsg.Tables[0].Rows[0]["dAngleInverse"].ToString()); 
            }
            catch (Exception ex)
            {
                throw new Exception("DirectionInfo数据库读取失败" + ex.Message.ToString());
            }
        }
        /*******************************************************************************/
        /*   更新数据库中的数据                                                        */
        /*******************************************************************************/
        public void DataUpdate(string sFileName, string sFormulaName)
        {
            string sConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                            + sFileName + ".accdb;Persist Security Info=False";
            string sSql = "";
            OleDbParameter[] ar_Para = null;

            try
            {
                sSql = "UPDATE [DirectionInfo] SET [name]=@name,"
                         + "[dEncodeDiameter] = @dEncodeDiameter,"
                         + "[dFactorX] = @dFactorX,"
                         + "[dFactorY] = @dFactorY,"
                         + "[iEncoderMode] = @iEncoderMode,"
                         + "[dXInverse] = @dXInverse,"
                         + "[dYInverse] = @dYInverse,"
                         + "[dFlyInverse] = @dFlyInverse,"
                         + "[dAngleInverse] = @dAngleInverse "      //最后一个不要加上逗号
                         + "where name = '" + sFormulaName + "'";

                ar_Para = new OleDbParameter[]{
                         new OleDbParameter("@name",OleDbType.VarChar),
                         new OleDbParameter("@dEncodeDiameter",OleDbType.VarChar),
                         new OleDbParameter("@dFactorX",OleDbType.VarChar),
                         new OleDbParameter("@dFactorY",OleDbType.VarChar),
                         new OleDbParameter("@iEncoderMode",OleDbType.VarChar),
                         new OleDbParameter("@dXInverse",OleDbType.VarChar),
                         new OleDbParameter("@dYInverse",OleDbType.VarChar),
                         new OleDbParameter("@dFlyInverse",OleDbType.VarChar),
                         new OleDbParameter("@dAngleInverse",OleDbType.VarChar),};

                ar_Para[0].Value = sFormulaName;
                ar_Para[1].Value = Convert.ToString(dEncodeDiameter);
                ar_Para[2].Value = Convert.ToString(dFactorX);
                ar_Para[3].Value = Convert.ToString(dFactorY);
                ar_Para[4].Value = Convert.ToString(iEncoderMode);
                ar_Para[5].Value = Convert.ToString(dXInverse);
                ar_Para[6].Value = Convert.ToString(dYInverse);
                ar_Para[7].Value = Convert.ToString(dFlyInverse);
                ar_Para[8].Value = Convert.ToString(dAngleInverse);

                ArrayUpdate(sConStr, sSql, ar_Para);
            }
            catch (Exception ex)
            {
                throw new Exception("DirectionInfo数据库写入失败：" + ex.Message.ToString());
            }
        }
        public static void ArrayUpdate(string sConStr, string sSql, OleDbParameter[] ar_Para)
        {
            // 连接数据集
            using (OleDbConnection connect = new OleDbConnection(sConStr))
            {
                // 对数据集的命令控制
                using (OleDbCommand cmd = new OleDbCommand(sSql, connect))
                {
                    if (ar_Para != null && ar_Para.Length > 0)
                        cmd.Parameters.AddRange(ar_Para);

                    if (connect.State == System.Data.ConnectionState.Closed)
                    {
                        connect.Open();
                    }

                    cmd.ExecuteNonQuery();

                    connect.Close();
                }
            }
        }
        /// <summary>
        /// 更新文件数据
        /// </summary>
        /// <param name="文件名"></param>
        /// <param name="列表名"></param>
        /// <param name="文件名"></param>
        /// <param name="更新数据"></param>
        public void update(string sFileName, string sFormulaName, string sDataName = null, string sData = null)
        {
            // 文件名
            string sConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                            + sFileName + ".accdb;Persist Security Info=False";
            OleDbParameter[] ar_Para = null;  // 可获取或设置参数
            string sSql = "";

            try
            {
                sSql = "UPDATE [DirectionInfo] SET [name]=@name,"
                         + "[dEncodeDiameter] = @dEncodeDiameter" // + sData + "," //@ID "      //最后一个不要加上逗号
                         + "where name = '" + sFormulaName + "'";

                ar_Para = new OleDbParameter[]{
                         new OleDbParameter("@name",OleDbType.VarChar),
                         new OleDbParameter("@dEncodeDiameter",OleDbType.VarChar) };

                ar_Para[0].Value = sFormulaName;
                ar_Para[1].Value = Convert.ToString(sData);

                // 连接数据集
                using (OleDbConnection connect = new OleDbConnection(sConStr))
                {
                    // 对数据集的命令控制
                    using (OleDbCommand cmd = new OleDbCommand(sSql, connect))
                    {
                        if (ar_Para != null && ar_Para.Length > 0)
                            cmd.Parameters.AddRange(ar_Para);

                        if (connect.State == System.Data.ConnectionState.Closed)
                        {
                            connect.Open();
                        }

                        cmd.ExecuteNonQuery();

                        connect.Close();
                    }
                }
            }
            catch {
            }
            finally
            {
                ShowOleDbException();
            }
        }

        /// <summary>
        /// 缺少数据源时生成
        /// </summary>
        public void ShowOleDbException()
        {
            string mySelectQuery = "SELECT column1 FROM table1";
            OleDbConnection myConnection =
               new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;DataSource=");
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, myConnection);

            try
            {
                myCommand.Connection.Open();
            }
            catch (OleDbException e)
            {
                string errorMessages = "";

                for (int i = 0; i < e.Errors.Count; i++)
                {
                    errorMessages += "Index #" + i + "\n" +
                                     "Message: " + e.Errors[i].Message + "\n" +
                                     "NativeError: " + e.Errors[i].NativeError + "\n" +
                                     "Source: " + e.Errors[i].Source + "\n" +
                                     "SQLState: " + e.Errors[i].SQLState + "\n";
                }

                System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
                log.Source = "My Application";
                log.WriteEntry(errorMessages);
                Console.WriteLine("An exception occurred. Please contact your system administrator.");
                Console.WriteLine(errorMessages);
            }
        }
        /// <summary>
        /// 数据库错误：https://docs.microsoft.com/zh-cn/dotnet/api/system.data.oledb.oledberror?view=netframework-4.8
        /// </summary>
        /// <param name="exception"></param>
        public void DisplayOleDbErrorCollection(OleDbException exception)
        {
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                Console.WriteLine("Index #" + i + "\n" +
                    "Message: " + exception.Errors[i].Message + "\n" +
                    "Native: " + exception.Errors[i].NativeError.ToString() + "\n" +
                    "Source: " + exception.Errors[i].Source + "\n" +
                    "SQL: " + exception.Errors[i].SQLState + "\n");
            }
            Console.ReadLine();
        }
    }
}
