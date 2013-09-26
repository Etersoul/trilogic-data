// <copyright file="Program.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic
{
    using System;

    using Gtk;

    /// <summary>
    /// Main class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            GLib.ExceptionManager.UnhandledException += (GLib.UnhandledExceptionArgs a) => 
            {
                Exception e = (Exception)a.ExceptionObject;
                Console.WriteLine(e.Message);
                new ExceptionDialog(e.InnerException.Message, e.InnerException.StackTrace);
            };

            Application.Run();
        }
    }
}
