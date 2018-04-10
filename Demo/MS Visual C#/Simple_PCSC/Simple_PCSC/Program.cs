using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Simple_PCSC
{
    static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Simple_PCSC());
        }
    }
}
