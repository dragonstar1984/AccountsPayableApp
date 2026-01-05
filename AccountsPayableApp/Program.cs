using AccountsPayableApp.Forms;
using System;
using System.Windows.Forms;

namespace AccountsPayableApp
{
    internal static class Program
    {
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 🔧 Initialize application configuration (DPI, fonts, etc.)
            ApplicationConfiguration.Initialize();

            // 🔐 Launch the login form first
            Application.Run(new LoginForm());
        }
    }
}