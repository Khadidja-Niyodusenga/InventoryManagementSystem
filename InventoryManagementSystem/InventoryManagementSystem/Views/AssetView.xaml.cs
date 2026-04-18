using InventoryManagementSystem.services;
using InventoryManagementSystem.models;
using System.Windows.Controls;
using System.Windows;

namespace InventoryManagementSystem.Views
{
    public partial class AssetsView : Page
    {
        private readonly AssetService _service = new AssetService();

        public AssetsView()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                AssetsDataGrid.ItemsSource = _service.GetAllAssets();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddAsset_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AssetFormWindow();
            if (dialog.ShowDialog() == true)
            {
                _service.AddAsset(dialog.Asset);
                RefreshGrid();
            }
        }
        private void EditAsset_Click(object sender, RoutedEventArgs e)
        {
            if (AssetsDataGrid.SelectedItem is Asset selected)
            {
                try
                {
                    var dialog = new AssetFormWindow(selected);
                    if (dialog.ShowDialog() == true)
                    {
                        _service.UpdateAsset(dialog.Asset);
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
                MessageBox.Show("Please select an asset to edit.");
            }
        }

        private void DeleteAsset_Click(object sender, RoutedEventArgs e)
        {
            if (AssetsDataGrid.SelectedItem is Asset selected)
            {
                var confirm = MessageBox.Show(
                    $"Delete {selected.DeviceName}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo);

                if (confirm == MessageBoxResult.Yes)
                {
                    _service.DeactivateAsset(selected.AssetID);
                    RefreshGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select an asset to delete.");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var keyword = txtSearch.Text.ToLower();
            var all = _service.GetAllAssets();
            AssetsDataGrid.ItemsSource = all.FindAll(a =>
                a.DeviceName.ToLower().Contains(keyword) ||
                a.SerialNumber.ToLower().Contains(keyword) ||
                a.Department.ToLower().Contains(keyword));
        }
    }
}