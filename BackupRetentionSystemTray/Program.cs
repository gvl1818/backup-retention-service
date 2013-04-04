using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BackupRetention
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
            //Application.Run(new BackupRetentionSystemTray());
            bool createdNew = false;
            Mutex mutex = null;
            try
            {
                mutex = new Mutex(true, "BackupRetentionSystemTray", out createdNew);
            }
            catch
            {
            }
            if (mutex == null || !createdNew)
            {
                MessageBox.Show("Another instance of BackupRetentionSystemTray is already running.", "Cannot start BackupRetentionSystemTray", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Application.Run(new BackupRetentionSystemTray());
            }
            finally
            {
                mutex.Close();
            }
        }
    }
}
