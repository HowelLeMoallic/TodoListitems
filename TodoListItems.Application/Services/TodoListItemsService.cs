using Microsoft.Extensions.Logging;
using TodoListItems.Application.DTO;
using TodoListItems.Application.Enum;
using TodoListItems.Application.Interfaces;
using TodoListItems.Infrastructure.Interfaces;
using TodoListItems.Infrastructure.Repositories;

namespace TodoListItems.Application.Services
{
    public class TodoListItemsService : ITodoListItemsService
    {
        private readonly ITodoListItemsRepository _repository;
        private readonly ILogger<TodoListItemsRepository> _logger;

        public TodoListItemsService(ITodoListItemsRepository repository, ILogger<TodoListItemsRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Retourne tout les todo list items
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<List<TODO_ItemDTO>> GetAllTODO_Items()
        {
            try
            {
                List<TODO_ItemDTO> results = _repository.GetAllTODO_Items()
                .Select(x =>
                new TODO_ItemDTO
                {
                    Title = x.Title,
                    Description = x.Description,
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    IdItem = x.IdItem,
                    todo_ItemsStatus = (TODO_ItemsStatus)x.IdStatus
                }).ToList();

                return new ServiceResponse<List<TODO_ItemDTO>>
                {
                    Code = ServiceResponseCode.Success,
                    Message = "Succès",
                    Data = results
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Erreur durant la récupération des todo list items : {0}", ex.Message));
                return new ServiceResponse<List<TODO_ItemDTO>>
                {
                    Code = ServiceResponseCode.Error,
                    Message = String.Format("Erreur durant la récupération des todo list items : {0}", ex.Message)
                };
            }
            
        }
    }
}
