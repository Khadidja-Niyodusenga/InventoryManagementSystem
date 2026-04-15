using System;
using System.Windows.Forms;
using inventory_system.data;

namespace inventory_system
{
    internal static class Program
    {
        /// <summary>
        ///  Application entry point
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 🧱 Initialize database
            DatabaseInitializer.Initializer();

            // 🧱 Run migrations safely
            DatabaseMigration.EnsureAssetsColumns();
            DatabaseMigration.EnsureUsersColumns();
            DatabaseMigration.EnsureAssignmentsColumns();

            // 🚀 Start application
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}