using System;
using System.Windows.Forms;
using BeerOverflowWindowsApp.Database;

namespace BeerOverflowWindowsApp
{
    static class Program
    {
        public static User defaultUser = new User { Username = "admin" };
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {       
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
