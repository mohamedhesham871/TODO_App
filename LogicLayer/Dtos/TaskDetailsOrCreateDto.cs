using LogicLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace PresentaionLayer.Dtos
{
    public class TaskDetailsOrCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = null!;
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
        public Status Status { get; set; } // Pending/InProgress/Completed [Enum]
        public Priority Priority { get; set; }// Low/Medium/High [Enum] 
        public DateTime? DueDate { get; set; }
    }
}
