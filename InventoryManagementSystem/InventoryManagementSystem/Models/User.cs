namespace inventory_system.models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int IsActive { get; set; } = 1; // 1 = active, 0 = deactivated
    }
}