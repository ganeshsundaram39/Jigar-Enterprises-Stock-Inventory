using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jigar_Enterprises_Stock_Inventory
{
    public partial class Mainform : MetroForm
    {
        List<string> imagelist;
        int j = 0;
        public Mainform()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\white.png"));
            this.Icon = Jigar_Enterprises_Stock_Inventory.Properties.Resources.jeicon;
            imagelist=new List<string>();
       
           for (int i = 1; i <= 94; i++) {
               imagelist.Add( Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Resources\\img ("+i+").png"));
           }
        
           timer1.Start();
           this.DoubleBuffered = true;
        }
    
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (j == 93)
            {
                j = 0;
            }

            pictureBox1.Image = Image.FromFile(imagelist[j]);
           
            ++j;

           
             }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Inward i = new Inward();
            i.ShowDialog();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            Outward i = new Outward();
            i.ShowDialog();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Stock i = new Stock();
            i.ShowDialog();
        }

       

    

  
      

    
    }
}
