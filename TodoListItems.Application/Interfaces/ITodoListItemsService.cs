using TodoListItems.Application.DTO;

namespace TodoListItems.Application.Interfaces
{
    public interface ITodoListItemsService
    {
        public ServiceResponse<List<TODO_ItemDTO>> GetAllTODO_Items();
        public ServiceResponse<List<TODO_ItemDTO>> GetFilterTODO_Items(FliterTodoListItem filter);
        public ServiceResponse<TODO_ItemDTO> UpdateTODO_Item(TODO_ItemDTO item);
        public ServiceResponse<Boolean> DeleteTODO_Item(int id);
    }
}
