namespace inventory_system.models
{
    public class AuditLog
    {
        public int LogID { get; set; }
        public string EntityType { get; set; }
        public int EntityID { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public string Timestamp { get; set; }
        public string Description { get; set; }
    }
}