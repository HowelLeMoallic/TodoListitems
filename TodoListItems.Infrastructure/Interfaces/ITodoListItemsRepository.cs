using TodoListItems.Domain.Models;

namespace TodoListItems.Infrastructure.Interfaces
{
    public interface ITodoListItemsRepository
    {
        public List<TODO_Item> GetAllTODO_Items();
    }
}
