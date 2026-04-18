using InventoryManagementSystem.models;
using InventoryManagementSystem.services;
using System.Windows;
using System.Windows.Controls;

namespace InventoryManagementSystem.Views
{
    public partial class UserView : UserControl
    {
        private readonly UserService _service = new UserService();

        public UserView()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                UsersDataGrid.ItemsSource = _service.GetAllUsers();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new UserFormWindow();

            if (dialog.ShowDialog() == true)
            {
                _service.AddUser(dialog.User);
                RefreshGrid();
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selected)
            {
                try
                {
                    var dialog = new UserFormWindow(selected);

                    if (dialog.ShowDialog() == true)
                    {
                        _service.UpdateUser(dialog.User);
                        RefreshGrid();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selected)
            {
                var confirm = MessageBox.Show(
                    $"Deactivate {selected.Name}?",
                    "Confirm",
                    MessageBoxButton.YesNo);

                if (confirm == MessageBoxResult.Yes)
                {
                    _service.DeactivateUser(selected.UserID);
                    RefreshGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select a user.");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var keyword = txtSearch.Text.ToLower();
            var all = _service.GetAllUsers();

            UsersDataGrid.ItemsSource = all.FindAll(u =>
                u.Name.ToLower().Contains(keyword) ||
                u.Department.ToLower().Contains(keyword) ||
                u.UserID.ToString().Contains(keyword));
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            RefreshGrid();
        }
        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // optional (can stay empty)
        }
    }

}