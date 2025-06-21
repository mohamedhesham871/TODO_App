namespace DataAccessLayer.Models
{
    public class TaskTable
    {

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Status { get; set; } = null!; // Pending/InProgress/Completed [Enum]
        public string Priority { get; set; } = null!; // Low/Medium/High [Enum]
        public DateTime? DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }
}
