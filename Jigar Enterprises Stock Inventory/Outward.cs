using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace Jigar_Enterprises_Stock_Inventory
{
    public partial class Outward : MetroForm
    {
        SQLiteConnection connection;
        string connectionString;
        string rowid;
        SQLiteCommand command;
  
        
        public Outward()
        {
            InitializeComponent();
            connectionString = Connection.getConnection();
            connection = new SQLiteConnection(connectionString);
            gridViewgenerate();
            update.Focus();

            this.DoubleBuffered = true;
            metrotab.SelectedTab = metroTabPage1;


         
        }

    
        private void gridViewgenerate()
        {
            try
            {

              
                SQLiteDataAdapter d = new SQLiteDataAdapter("select rowid as Id,Productname as Product from ProductTbl",connection);
                DataTable dt=new DataTable();
                d.Fill(dt);
                gridview.DataSource = dt;
                gridview2.DataSource = dt;
               
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

        private void gridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview.Rows[e.RowIndex];
                    rowid = row.Cells["ID"].Value.ToString();
                    connection.Open();
                    SQLiteDataAdapter d = new SQLiteDataAdapter("select Productname ,Quantity,Price,Totalprice from ProductTbl where rowid='"+rowid+"'", connection);
          
                      DataTable dt=new DataTable();
                d.Fill(dt);
                productname.Text = dt.Rows[0][0].ToString();
                quantity.Text= dt.Rows[0][1].ToString();
                price.Text = dt.Rows[0][2].ToString();
                total.Text = dt.Rows[0][3].ToString();
                }
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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
          SQLiteDataAdapter    sda= new SQLiteDataAdapter("SELECT count(*) FROM ProductTbl", connection);
                DataTable       dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() != "0")
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


                                    connection.Open();
                                    command = connection.CreateCommand();
                                    command.CommandText = "SELECT count(*) FROM ProductTbl where Productname='" + productname.Text.ToLower() + "' and NOT rowid='" + rowid + "'";


                                    var i = command.ExecuteScalar();




                                    if (Convert.ToInt16(i) != 1)
                                    {

                                        command.CommandText = "Update ProductTbl  set Productname='" + productname.Text.ToLower() + "',Price='" + price.Text + "',Quantity='" + quantity.Text + "',Totalprice='" + total.Text + "' where rowid='" + rowid + "' ;";

                                        // And execute this again
                                        command.ExecuteNonQuery();

                                        MetroMessageBox.Show(this, "One item updated..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);




                                    }
                                    else
                                    {
                                        MetroMessageBox.Show(this, "Item with same name already exist..!!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        productname.Focus();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                connection.Close();
                                int a = gridview.CurrentCell.RowIndex;
                                gridViewgenerate();
                                gridview.CurrentCell = gridview.Rows[a].Cells[0];
                            }
                        }
                        else
                        {
                            productname.Clear();
                            quantity.Clear();
                            total.Clear();
                            price.Clear();
                        }
        }

        private void gridview_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.gridview.Rows[e.RowIndex];
                    rowid = row.Cells["ID"].Value.ToString();
                    connection.Open();
                    SQLiteDataAdapter d = new SQLiteDataAdapter("select Productname ,Quantity,Price,Totalprice from ProductTbl where rowid='" + rowid + "'", connection);

                    DataTable dt = new DataTable();
                    d.Fill(dt);
                    productname.Text = dt.Rows[0][0].ToString();
                    quantity.Text = dt.Rows[0][1].ToString();
                    price.Text = dt.Rows[0][2].ToString();
                    total.Text = dt.Rows[0][3].ToString();
                }
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

   

        private void gridview2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        

          SQLiteDataAdapter sda=new SQLiteDataAdapter("SELECT count(*) FROM ProductTbl",connection);
           DataTable dt=new DataTable();
           sda.Fill(dt);
          string i= dt.Rows[0][0].ToString();

       
            if (Convert.ToInt16(i) > 0)
            {

                if (MetroMessageBox.Show(this, "Sure you wanna delete all data of this item..??", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    try
                    {
                        if (e.RowIndex >= 0)
                        {
                            DataGridViewRow row = this.gridview.Rows[e.RowIndex];

                            SQLiteDataAdapter m = new SQLiteDataAdapter();


                            m.DeleteCommand = new SQLiteCommand("Delete from ProductTbl where rowid='" + row.Cells["ID"].Value.ToString() + "'", connection);

                            connection.Open();
                            m.DeleteCommand.ExecuteNonQuery();

                            connection.Close();

                            int a = gridview2.CurrentCell.RowIndex;
                            gridViewgenerate();
                            if (a != 0)
                            {
                                gridview.CurrentCell = gridview.Rows[a - 1].Cells[0];
                            }
                        }
                    }

                    catch (Exception ex)
                    {

                        MetroMessageBox.Show(this, ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        connection.Close();
                       sda= new SQLiteDataAdapter("SELECT count(*) FROM ProductTbl", connection);
                       dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString()=="0")
                        {
                            productname.Clear();
                            price.Clear();
                            total.Clear();
                            quantity.Clear();
                        }
                    }
                }
            }
        }

        private void quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void total_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
        private void totalPrice(string quantity, string price)
        {

            total.Text = (Convert.ToInt64(quantity) * Convert.ToSingle(price)).ToString("0.00");
        }

        private void quantity_TextChanged(object sender, EventArgs e)
        {
            totalcal();
        }

      

     
    }
}
