using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMEmu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(ErrorForm_UIThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ErrorForm_UIUnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void ErrorForm_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            ErrorForm form = new ErrorForm();
            form.SetMessage(t.Exception.ToString());
            form.ShowDialog();
            Application.Exit();
        }
        private static void ErrorForm_UIUnhandledException(object sender, UnhandledExceptionEventArgs t)
        {
            Exception ex = (Exception)t.ExceptionObject;

            ErrorForm form = new ErrorForm();
            form.SetMessage(ex.ToString());
            form.ShowDialog();
            Application.Exit();
        }
    }
}
