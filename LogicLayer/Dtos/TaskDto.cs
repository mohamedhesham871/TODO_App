using LogicLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Dtos
{
    public  class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Status Status { get; set; } 
        public Priority Priority { get; set; } 
        public DateTime? DueDate { get; set; }
    }
}
