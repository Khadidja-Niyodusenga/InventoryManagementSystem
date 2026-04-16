using System;

namespace inventory_system.models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }
        public int AssetID { get; set; }
        public int UserID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string ConditionBefore { get; set; }
        public string ConditionAfter { get; set; }

        public int IsActive { get; set; }

        public string Status { get; set;}
    }
}