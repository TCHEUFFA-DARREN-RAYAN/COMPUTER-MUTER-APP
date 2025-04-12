using System;
using System.Windows.Forms;

/// <summary>
/// Mute Scheduler Application
/// Author: TCHEUFFA DARREN
/// Date: April 2025
/// </summary>
namespace MuteScheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
