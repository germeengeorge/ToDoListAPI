using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.Models
{
    public class todolist
    {
        [Required]
        public int id {  get; set; }
        [Required]
        [StringLength(100,ErrorMessage = "Title can't be longer than 100 characters")]
        public string title {  get; set; }
        public string? description { get; set; }
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [Required]
        public bool IsCompleted {  get; set; }
        [Required]
        [RegularExpression("^(?i)(Low|Medium|High)$", ErrorMessage = "Priority must be Low, Medium, or High.")]
        public string Priority {  get; set; }
    }
}
