using TodoListItems.Domain.Models;

namespace TodoListItems.Infrastructure.Interfaces
{
    public interface ITodoListItemsRepository
    {
        public List<TODO_Item> GetAllTODO_Items();
        public TODO_Item GetTODO_ItemById(int id);
        public TODO_Item UpdateTODO_Item(TODO_Item item);
    }
}
