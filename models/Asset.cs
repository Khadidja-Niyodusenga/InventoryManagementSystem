namespace inventory_system.models
{
    public class Asset
    {
        public int AssetID { get; set; }
        public string DeviceName { get; set; }
        public string Type { get; set; }
        public string SerialNumber { get; set; }
        public string Specifications { get; set; }
        public string ConditionStatus { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
    }
}