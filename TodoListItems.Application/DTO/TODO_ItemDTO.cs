using TodoListItems.Application.Enum;

namespace TodoListItems.Application.DTO
{
    public class TODO_ItemDTO
    {
        public required string Title { get; set; }

        public string? Description { get; set; }

        public TODO_ItemsStatus todo_ItemsStatus { get; set; }

        public int CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public int IdItem { get; set; }
    }
}
