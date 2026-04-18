using InventoryManagementSystem.models;
using System.Windows;

namespace InventoryManagementSystem.Views
{
    public partial class AssetFormWindow : Window
    {
        public Asset Asset { get; private set; }

        // Add mode
        public AssetFormWindow()
        {
            InitializeComponent();
        }

        // Edit mode — pre-fills the form
        public AssetFormWindow(Asset existing)
        {
            InitializeComponent();
            Asset = existing;
            txtDeviceName.Text = existing.DeviceName;
            txtType.Text = existing.Type;
            txtSerial.Text = existing.SerialNumber;
            txtSpecs.Text = existing.Specifications;
            txtDepartment.Text = existing.Department;

            foreach (var item in cmbCondition.Items)
            {
                if (item is System.Windows.Controls.ComboBoxItem c &&
                    c.Content.ToString() == existing.ConditionStatus)
                {
                    cmbCondition.SelectedItem = item;
                    break;
                }
            }

            foreach (var item in cmbStatus.Items)
            {
                if (item is System.Windows.Controls.ComboBoxItem s &&
                    s.Content.ToString() == existing.Status)
                {
                    cmbStatus.SelectedItem = item;
                    break;
                }
            }
        }

        

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeviceName.Text) ||
                string.IsNullOrWhiteSpace(txtSerial.Text))
            {
                MessageBox.Show("Device Name and Serial Number are required.");
                return;
            }

            Asset = new Asset
            {
                AssetID = Asset?.AssetID ?? 0,
                DeviceName = txtDeviceName.Text,
                Type = txtType.Text,
                SerialNumber = txtSerial.Text,
                Specifications = txtSpecs.Text,
                ConditionStatus = (cmbCondition.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString(),
                Status = (cmbStatus.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString(), // ← add this
                Department = txtDepartment.Text
            };

            DialogResult = true;
            Close();
        }
    }
}