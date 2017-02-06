 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jigar_Enterprises_Stock_Inventory
{
    class Connection
    {

        public static string getConnection()
        {


            string relativePath = @"ProductDB.db";
            string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string absolutePath = System.IO.Path.Combine(currentPath, relativePath);
            absolutePath = absolutePath.Remove(0, 6);//this code is written to remove file word from absolute path
            string connectionString = string.Format("Data Source={0};", absolutePath);

            return connectionString;

        }

    }


}
