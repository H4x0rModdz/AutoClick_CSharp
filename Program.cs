using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClick
{
    static class Program
    {
        /// PLEASE READ THIS BEFORE USE MY PROGRAM! 
        /// <important>
        /// this program is made by me and updated to the latest version to improve ur skills with automation process development. Don't sell this program, but transfer it to many developers, this helps us to be better. Thanks!
        /// </important>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoClickProj());
        }
    }
}
