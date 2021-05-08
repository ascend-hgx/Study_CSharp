using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class GetSocket
{
    /// <summary>
    /// 使用TCP协议，连接socket，输入IP地址、端口号，返回socket
    /// </summary>
    /// <param name="server"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    public static Socket ConnectSocket(string server, int port)
    {
        Socket s = null;
        IPHostEntry hostEntry = null;

        // Get host related information.
        hostEntry = Dns.GetHostEntry(server);

        // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
        // an exception that occurs when the host IP Address is not compatible with the address family
        // (typical in the IPv6 case).
        foreach (IPAddress address in hostEntry.AddressList)
        {
            IPEndPoint ipe = new IPEndPoint(address, port);
            // ProtocolType.Tcp 使用TCP协议连接，TCP需要先握手确认才能收发信号
            Socket tempSocket =
                new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            if (tempSocket.Connected)
            {
                s = tempSocket;
                break;
            }
            else
            {
                tempSocket.Connect(ipe);    // tempSocket.Connect(server, port);
                continue;
            }
        }
        return s;
    }

    // This method requests the home page content for the specified server.
    /// <summary>
    /// 输入：对应服务的IP地址或名字、端口号、请求命令
    /// 返回：服务器返回命令
    /// </summary>
    /// <param name="server"></param>
    /// <param name="port"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string SocketSendReceive(string server, int port, string request = null)
    {
        // 如果请求命令为空
        if(request == null)
            request = "GET / HTTP/1.1\r\nHost: " + server +
                "\r\nConnection: Close\r\n\r\n";

        Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
        Byte[] bytesReceived = new Byte[256];
        string page = "";

        // Create a socket connection with the specified server and port.
        using (Socket s = ConnectSocket(server, port))
        {

            if (s == null)
                return ("Connection failed");

            // Send request to the server.发送文件
            s.Send(bytesSent, bytesSent.Length, 0);

            // Receive the server home page content.
            int bytes = 0;
            page = "Default HTML page on " + server + ":\r\n";

            bytesSent = Encoding.ASCII.GetBytes("last");
            s.Send(bytesSent, bytesSent.Length, 0);
            // The following will block until the page is transmitted.
            do
            {
                // 接收数据
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);
        }

        return page;
    }
    
    /* GetSocket getSocket = new GetSocket();
     * string[] str = { "127.0.0.1" };
     * getSocket.test(str);
     */
    public void test(string[] args = null)
    {
        Connect("192.168.250.111", "Hello, i love you");
        string host;
        int port = 4006;

        if (args == null || args.Length == 0)
            // If no server name is passed as argument to this program, 
            // use the current host name as the default.
            host = Dns.GetHostName();
        else
            host = args[0];

        string result = SocketSendReceive(host, port);
        Console.WriteLine("你好呀");
    }

    // 可以连接到树莓派
    void Connect(String server, String message, Int32 port = 4006)
    {
        try
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer
            // connected to the same address as specified by the server, port
            // combination.
            TcpClient client = new TcpClient(server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();
            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", data);
            stream.Write(data, 0, data.Length);
            stream.Write(data, 0, data.Length);
            stream.Write(data, 0, data.Length);

            Thread.Sleep(100);
            message = "exit";
            data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\n Press Enter to continue...");
        Console.Read();
    }
}