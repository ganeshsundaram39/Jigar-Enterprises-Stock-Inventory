using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jigar_Enterprises_Stock_Inventory
{
    public partial class Stock : MetroForm
    {
        SQLiteConnection connection;
        string connectionString;
        public Stock()
        {
            InitializeComponent();

            connectionString = Connection.getConnection();
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteDataAdapter d = new SQLiteDataAdapter("select rowid as ID,Productname as \"Product name\" ,Quantity,Price,Totalprice as \"Total Price\" from ProductTbl ", connection);

            DataTable dt = new DataTable();
            d.Fill(dt);
            gridview.DataSource = dt;
        }
    }
}
