using System;
using System.Windows.Forms;
using inventory_system.models;
using inventory_system.services;

namespace inventory_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Make grid look clean
            dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadUsers();
        }

        // 🔄 LOAD USERS INTO GRID
        private void LoadUsers()
        {
            var service = new UserService();
            var users = service.GetAllUsers();

            dataGridViewUsers.DataSource = users;
        }

        // ➕ ADD USER
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var service = new UserService();

            var user = new User
            {
                Name = "User " + DateTime.Now.Ticks,
                Department = "IT"
            };

            service.AddUser(user);

            MessageBox.Show("✔ User added!");

            LoadUsers();
        }

        // 🔄 REFRESH USERS
        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        // ✏️ UPDATE SELECTED USER
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.CurrentRow == null)
            {
                MessageBox.Show("Select a user first");
                return;
            }

            var user = (User)dataGridViewUsers.CurrentRow.DataBoundItem;

            user.Name = "UPDATED USER";
            user.Department = "HR";

            var service = new UserService();
            service.UpdateUser(user);

            MessageBox.Show("✔ User updated!");

            LoadUsers();
        }

        // ❌ DEACTIVATE USER (SOFT DELETE)
        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.CurrentRow == null)
            {
                MessageBox.Show("Select a user first");
                return;
            }

            var user = (User)dataGridViewUsers.CurrentRow.DataBoundItem;

            var service = new UserService();
            service.DeactivateUser(user.UserID);

            MessageBox.Show("✔ User deactivated!");

            LoadUsers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}