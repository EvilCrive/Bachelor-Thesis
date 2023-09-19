using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SAPbouiCOM;

namespace btnPrintOnForm
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //public SAPbouiCOM.Application oApp;


        [STAThread]
        static void Main(string[] args)
        {
            Events ev = null;
            ev = new Events();
            System.Windows.Forms.Application.Run();
        }



    }
}
