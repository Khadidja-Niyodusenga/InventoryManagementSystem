using System.ComponentModel;
using inventory_system.services;

namespace InventoryManagementSystem.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly DashboardService _service;

        public DashboardViewModel()
        {
            _service = new DashboardService();
            LoadData();
        }

        private int _totalAssets;
        private int _assignedAssets;
        private int _availableAssets;
        private int _damagedAssets;

        public int TotalAssets
        {
            get => _totalAssets;
            set { _totalAssets = value; OnPropertyChanged(nameof(TotalAssets)); }
        }

        public int AssignedAssets
        {
            get => _assignedAssets;
            set { _assignedAssets = value; OnPropertyChanged(nameof(AssignedAssets)); }
        }

        public int AvailableAssets
        {
            get => _availableAssets;
            set { _availableAssets = value; OnPropertyChanged(nameof(AvailableAssets)); }
        }

        public int DamagedAssets
        {
            get => _damagedAssets;
            set { _damagedAssets = value; OnPropertyChanged(nameof(DamagedAssets)); }
        }

        public void LoadData()
        {
            TotalAssets = _service.GetTotalAssets();
            AssignedAssets = _service.GetAssignedAssets();
            AvailableAssets = _service.GetAvailableAssets();
            DamagedAssets = _service.GetDamagedAssets();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}