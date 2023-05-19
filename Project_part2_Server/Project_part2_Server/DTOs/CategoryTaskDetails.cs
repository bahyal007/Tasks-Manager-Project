using TaskModel;

namespace Project_part2_Server.DTOs
{
    public class CategoryTaskDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }
    }
}
