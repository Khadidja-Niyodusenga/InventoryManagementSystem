using System.Text;
using System.Windows;
using InventoryManagementSystem.Views;
using inventory_system.data;
namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseInitializer.Initializer();
            MainFrame.Navigate(new Views.DashboardView());
        }
        private void Assets_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AssetsView());
        }
    }
}