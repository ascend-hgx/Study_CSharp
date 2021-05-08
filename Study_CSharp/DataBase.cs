using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace PublicFunctionLib
{
    // 用于基础数据库测试
    class DataBase
    {
        //先打开两个类库文件
        SqlConnection connection;  // = new SqlConnection();
        public string connectionString = "";       // 当前数据库连接串

        public DataBase() { }
        public DataBase(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void file(string fileName)
        {
            try {
                connectionString = "server=.;database=" + fileName + ";uid =;pwd=";
                connection = new SqlConnection(connectionString);
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("文件读取成功");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("文件打开失败");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
