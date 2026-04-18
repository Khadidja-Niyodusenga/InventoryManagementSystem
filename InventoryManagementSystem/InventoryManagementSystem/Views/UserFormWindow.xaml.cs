using InventoryManagementSystem.models;
using System.Windows;

namespace InventoryManagementSystem.Views
{
    public partial class UserFormWindow : Window
    {
        public User User { get; private set; }

        // ➕ Add mode
        public UserFormWindow()
        {
            InitializeComponent();
        }

        // ✏️ Edit mode — pre-fill form
        public UserFormWindow(User existing)
        {
            InitializeComponent();

            User = existing;

            txtName.Text = existing.Name;
            cmbDepartment.Text = existing.Department;
            chkIsActive.IsChecked = existing.IsActive == 1;
        }

        // 💾 Save
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbDepartment.Text) ||
                cmbDepartment.Text == "Select Department")
            {
                MessageBox.Show("Department is required.");
                return;
            }

            User = new User
            {
                UserID = User?.UserID ?? 0,
                Name = txtName.Text,
                Department = cmbDepartment.Text,
                IsActive = (chkIsActive.IsChecked == true) ? 1 : 0
            };

            DialogResult = true;
            Close();
        }

        // ❌ Cancel (optional but good practice)
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}