using Microsoft.AspNetCore.Mvc;
using TodoListItems.Application.DTO;
using TodoListItems.Application.Interfaces;

namespace TodoListItems.BlazorServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListItemsController : ControllerBase
    {
        private readonly ITodoListItemsService _todoListItemsService;

        public TodoListItemsController(ITodoListItemsService todoListItemsService)
        {
            _todoListItemsService = todoListItemsService;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<TODO_ItemDTO>>> GetAllTODO_Items()
        {
            return _todoListItemsService.GetAllTODO_Items();
        }

        [HttpPost]
        public ActionResult<ServiceResponse<List<TODO_ItemDTO>>> GetFilterTODO_Items(FliterTodoListItem filter)
        {
            return _todoListItemsService.GetFilterTODO_Items(filter);
        }

        [HttpPut]
        public ActionResult<ServiceResponse<TODO_ItemDTO>> UpdateTODO_Item(TODO_ItemDTO item)
        {
            return _todoListItemsService.UpdateTODO_Item(item);
        }
    }
}
