using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jigar_Enterprises_Stock_Inventory
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
              Process[] result = Process.GetProcessesByName("Jigar Enterprises Stock Inventory");
            if (result.Length > 1)
            {
                System.Environment.Exit(0);
            }
            else
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Loading());
          Application.Run(new Mainform());
 
   }

}
    }
}
