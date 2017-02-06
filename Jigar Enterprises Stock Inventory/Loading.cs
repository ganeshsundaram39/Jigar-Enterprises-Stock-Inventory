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
    public partial class Loading : Form
    {
        Image image ,image1;
        Rectangle rect,rect1;
        int counter = 0;
        public Loading()
        {
            InitializeComponent();

            image =Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Resources\\leave.png"));
           

            image1 =Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Resources\\jigar.png"));
           
            rect = new Rectangle(50, 100, 120, 80);
            rect1 = new Rectangle(345, 0, 315, 95);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }
        Graphics g, g1;
        private void loading_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImage(image, rect);

           g1 = e.Graphics;
            g.DrawImage(image1, rect1);


        }
        int j = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (j == 0)
            {
               
                timer3.Start();
               
                timer3.Stop();
                j = 1;
            }

                counter++;
           
            if (counter % 2 == 0)
            {
                
                    rect.X += 9;

            
        
            }
            else
            {
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
              
          
            }
            rect.Y += 4;

            if (counter == 40)  //or whatever your limit is
            {
                timer1.Stop();
           
               
                timer2.Tick += ChangeOpacity;
                timer2.Start();
        
            }
                Invalidate();
        }



        void ChangeOpacity(object sender, EventArgs e)
        {
            this.Opacity -= .10; //replace.10 with whatever you want
            if (this.Opacity == 0)
            {
                timer1.Stop();
                this.Close();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < 265; i++)
            {
                rect1.Y = i;


            }
        }

   

    }
}
