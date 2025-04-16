using Microsoft.VisualBasic;

namespace ToDoListAPI.Models
{
    public class todolist
    {
        public int id {  get; set; }
        public string title {  get; set; }
        public string? description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted {  get; set; }
        public string Priority {  get; set; }
    }
}
