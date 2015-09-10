using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;

namespace github
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionOptions conn = new ConnectionOptions();
            conn.Username = "kingsley";//登入远端电脑的账户
            conn.Password = "111111";//登入远端电脑的密码
            ManagementPath path = new ManagementPath();

            //其中root\cimv2是固定写法
            path.Path = @"\\10.0.1.36\root\cimv2";
            ManagementScope ms = new ManagementScope();
            ms.Options = conn;
            ms.Path = path;
            ms.Connect();
            //这里的例子读取文件的最后修改日期
            ObjectQuery query = new ObjectQuery("SELECT * FROM CIM_DataFile WHERE Name = 'C:\\\\KVOffice.txt'");
            ManagementObjectSearcher  searcher = new ManagementObjectSearcher(ms,query);
            ManagementObjectCollection collection = searcher.Get();
            string time = "";
            foreach (ManagementObject obj in collection)
            {
                time = obj.Properties["LastModified"].Value.ToString().Substring(0,14);
            }
            Console.WriteLine(time);
            Console.ReadLine();
        }
    }
}
