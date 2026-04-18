using System.Text;
using System.Windows;
using InventoryManagementSystem.Views;
using InventoryManagementSystem.data;
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

        private void User_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UserView());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}