using System.Configuration;
using System.Data;
using System.Windows;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 🔥 Run database migrations before anything else
            DatabaseMigration.EnsureUsersColumns();
            DatabaseMigration.EnsureAssetsColumns();
            DatabaseMigration.EnsureAssignmentsColumns();
        }
    }

}
