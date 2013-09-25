using System;
using Gtk;

namespace Trilogic
{
    class MainClass
    {
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
