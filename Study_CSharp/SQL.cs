using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 使用SQL数据库操作
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;                // 保存accdb时使用   

namespace PublicFunctionLib
{
    class MySQL
    {
        // 需要现在“视图”》“服务器资源管理器”中添加本地数据库，对应数据库属性中“连接字符串”复制给connect string
        string connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\ECHO\DOCUMENTS\MYSQL_SERVER.MDF;Integrated Security=True;Connect Timeout=30";
        SqlConnection sqlCnt;       // 数据库连接
        SqlCommand command;         // 数据库命令控制

        public MySQL()
        {
            try
            {
                sqlCnt = new SqlConnection(connectString);
                sqlCnt.Open();

                // 以下可以直接使用这个函数：SqlCommand command = sqlCnt.CreateCommand(); 
                command = new SqlCommand();
                command.Connection = sqlCnt;            // 绑定SqlConnection对象
                command.CommandType = CommandType.Text;

                // 执行SQL命令（也可以这样重复使用）
                command = new SqlCommand("CREATE TABLE category(id int IDENTITY(1, 1) PRIMARY KEY not null,cname char(50))", sqlCnt);
                command.ExecuteNonQuery();

                //// 设定控制命令，并执行。在new的写入命令也可以，但目前这个如果已经有这个表格再添加会停止这
                //command.CommandText = "CREATE TABLE 表格2(text int IDENTITY(1, 1) PRIMARY KEY not null,cname char(50))";
                //command.ExecuteNonQuery();

                Console.WriteLine("数据库初始化成功");
            }

            catch { }
        }

        /// <summary>
        /// 输入数据库控制命令，并执行
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string commend(string cmd)
        {
            /* ExecuteNonQuery 返回执行命令后影响的行数++
             * ExecuteNonQueryAsync 和ExecuteNonQuery一致，不同在意异步处理
             * ExecuteReader 执行查询，返回查询结果
             * ExecuteScalar 执行查询，并返回由查询返回的结果集中的第一行的第一列，如果结果集为空，则为 null
             * ExecuteXmlReader 执行查询，将查询结果返回到一个XmlReader对象中
             */
            try
            {
                command.CommandText = cmd;
                //command.ExecuteNonQuery();

                if (Convert.ToInt32(command.ExecuteNonQuery()) > 0)
                    Console.WriteLine("Error");
            }
            catch { }

            return "";
        }

        /// <summary>
        /// 对category执行写入数据，加上我以后用于添加用户
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="connectionString"></param>
        public void write(int id, string name = "")
        {
            // 需要先执行更改为可以写入
            commend("set identity_insert category on");

            //INSERT INTO users(id, name) VALUES(1, 'name')
            string commandText = "INSERT INTO category(id, cname) VALUES(@id, '" + name + "')";

            SqlParameter[] ar_Para = new SqlParameter[] {
                new SqlParameter("@id", SqlDbType.Int),
                new SqlParameter("@cname", SqlDbType.NChar)
            };
            ar_Para[0].Value = id;
            ar_Para[1].Value = name;

            command.Parameters.Clear();             // 需要先清空，不然会报错提示已声明
            command.Parameters.AddRange(ar_Para);

            command.CommandText = commandText;

            //command.Parameters.Add("@id", SqlDbType.Int);   // 添加参数及其类型
            //command.Parameters["@id"].Value = id;           // 设定更改后的参数值
            //command.Parameters[1].Value = name;

            // Use AddWithValue to assign Demographics.
            // SQL Server will implicitly convert strings into XML.
            //command.Parameters.AddWithValue("@name", name);

            try
            {
                Int32 rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("RowsAffected: {0}", rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 删除对应id的一行数据
        /// </summary>
        /// <param name="id"></param>
        public void delete(int id)
        {
            //DELETE FROM classics WHERE title='Little Dorrit';
            commend("DELETE FROM category WHERE id = '" + id + "'");
        }

        // 更新第几个id数据为什么
        public void update(int id, string name)
        {
            string commandText = "UPDATE category SET cname = @cname "
        + "WHERE id = @id;";

            SqlParameter[] ar_Para = new SqlParameter[] {
                new SqlParameter("@id", SqlDbType.Int),
                new SqlParameter("@cname", SqlDbType.NChar)
            };
            ar_Para[0].Value = id;
            ar_Para[1].Value = name;

            command.Parameters.AddRange(ar_Para);

            command.CommandText = commandText;

            // 创建表格
            command.Parameters.Add("@id", SqlDbType.Int);   // 添加参数及其类型
            command.Parameters["@id"].Value = id;           // 设定更改后的参数值
            command.Parameters[1].Value = name;

            // Use AddWithValue to assign Demographics.
            // SQL Server will implicitly convert strings into XML.
            command.Parameters.AddWithValue("@name", name);

            try
            {
                Int32 rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("RowsAffected: {0}", rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 没毛病，下载是可以正常读取数据出来
        public void reader(string queryString = "", string connectionString = "")
        {
            if (queryString.Length > 0)
                command.CommandText = "select * from category where id = \'" + queryString + "\'";
            else
                command.CommandText = "select * from category";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // 读取数据库单行长度
                for (int i = 0; i < reader.VisibleFieldCount; i++)
                    Console.WriteLine(String.Format("{0}", reader[i]));
                // 数据库不为空
                //if (reader.HasRows)
                //{
                //    Console.WriteLine(String.Format("{0}", reader[1]));
                //}
            }

            reader.Close();
        }
        /// <summary>
        /// 数据库命令测试
        /// </summary>
        public void commandTest()
        {
            string str = "";
            while (str != "exit")
            {
                Console.WriteLine("请输入你的控制命令");

                str = Console.ReadLine();
                if (str == "read")
                {
                    string cmd = Console.ReadLine();
                    reader(cmd);
                }
                else if (str == "write")
                {
                    write(Convert.ToInt32(Console.ReadLine()), Console.ReadLine());
                }
                else if (str == "delete")
                    delete(Convert.ToInt32(Console.ReadLine()));
                else if (str == "update")
                    update(Convert.ToInt32(Console.ReadLine()), Console.ReadLine());
                else
                    commend(str);
            }
        }

        // 析构函数，不用添加public
        ~MySQL()
        {
            try
            {
                command.Dispose();
                sqlCnt.Close();
            }
            catch { }
        }
    }
}