using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Jigar_Enterprises_Stock_Inventory
{
    public partial class Inward : MetroForm
    {
        SQLiteConnection connection;
        SQLiteCommand command;
  
        string connectionString;
        public Inward()
        {
            InitializeComponent();
         connectionString= Connection.getConnection();
     
         connection = new SQLiteConnection(connectionString);
         autoComplete();
         this.DoubleBuffered = true;
            }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void autoComplete()
        {
            productname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            productname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            try
            {
                connection.Open();
                SQLiteCommand s = new SQLiteCommand("select Productname from Productlist", connection);
                SQLiteDataReader reader = s.ExecuteReader();
                while (reader.Read())
                {
                    coll.Add(reader.GetString(reader.GetOrdinal("Productname")));
                }

                productname.AutoCompleteCustomSource = coll;
            }
            catch (Exception ex)
            {

                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                connection.Close();

            }
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {

                if (productname.Text == "")
                {
                    MetroMessageBox.Show(this, "Product name cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    productname.Focus();
                }
                else if (quantity.Text == "")
                {
                    MetroMessageBox.Show(this, "Quantity cannot be empty..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    quantity.Focus();
                }

                else
                {
                   

                    // open the connection:
                    connection.Open();
                 
                    command = connection.CreateCommand(); 
                    command.CommandText = "SELECT count(*) FROM ProductTbl where Productname='"+productname.Text.ToLower()+"'";
               
                    // Now the SQLiteCommand object can give us a DataReader-Object:
                    var i = command.ExecuteScalar();
                
             


                      if ( Convert.ToInt16(i) !=1)
                      {
                          command.CommandText = "SELECT count(*) FROM Productlist where Productname='" + productname.Text.ToLower() + "'";

                          
                          var a = command.ExecuteScalar();


                          if (Convert.ToInt16(a) != 1)
                          {

                              command.CommandText = "INSERT INTO Productlist VALUES ('" + productname.Text.ToLower() + "');";

                             
                              command.ExecuteNonQuery();
                          }
                          string totalout;
                          if (price.Text != "")
                          {
                              totalout = (Convert.ToInt64(price.Text) * Convert.ToInt64(quantity.Text)).ToString();
                          }
                          else 
                          { 
                              totalout = "";
                          }
                          command.CommandText = "INSERT INTO ProductTbl VALUES ('" + productname.Text.ToLower() + "','" + price.Text + "','" + quantity.Text + "','" + totalout+ "');";

                          
                          command.ExecuteNonQuery();
                       
                          MetroMessageBox.Show(this, "One Item added..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          productname.Clear();
                          quantity.Clear();
                          price.Clear();
                          total.Clear();
                          productname.Focus();
                      }
                      else
                      {
                          MetroMessageBox.Show(this, "Item already added..!! Check Outward..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          productname.Focus();
                      }
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                connection.Close();
                autoComplete();
            }

        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void total_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void totalPrice(string quantity, string price)
        {

            total.Text = (Convert.ToInt64(quantity) * Convert.ToSingle(price)).ToString("0.00");
        }

        private void quantity_TextChanged(object sender, EventArgs e)
        {
            totalcal();

        }

        private void totalcal()
        {

            total.Clear();
            if (quantity.Text != "" && price.Text != "")
                totalPrice(quantity.Text, price.Text);
            if (quantity.Text == "" || price.Text == "")
                total.Clear();
        }

        private void price_TextChanged(object sender, EventArgs e)
        {
            totalcal();
        }

     
     
    }
}
