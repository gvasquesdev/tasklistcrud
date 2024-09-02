using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace TaskList.Models
{
    
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? IsCompleted { get; set; } = "pendente";
        public DateTime? CompletionDate { get; set; }
    }
}