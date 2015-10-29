using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SAS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMain());
            Application.Run(new Main());
        }
    }
}
