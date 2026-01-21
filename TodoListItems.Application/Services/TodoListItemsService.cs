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

        /// <summary>
        /// Retourne tout les todo list items filtrés
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<List<TODO_ItemDTO>> GetFilterTODO_Items(FliterTodoListItem filter)
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

                if (filter != null)
                {
                    if (filter.IdItemStatus != null)
                    {
                        results = results.Where(x => (int)x.todo_ItemsStatus == filter.IdItemStatus).ToList();
                    }
                }

                return new ServiceResponse<List<TODO_ItemDTO>>
                {
                    Code = ServiceResponseCode.Success,
                    Message = "Succès",
                    Data = results
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Erreur durant la récupération des todo list items filtrés : {0}", ex.Message));
                return new ServiceResponse<List<TODO_ItemDTO>>
                {
                    Code = ServiceResponseCode.Error,
                    Message = String.Format("Erreur durant la récupération des todo list items filtrés : {0}", ex.Message)
                };
            }

        }

        /// <summary>
        /// Met à jour l'item
        /// </summary>
        /// <param name="item">item à mettre à jour</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResponse<TODO_ItemDTO> UpdateTODO_Item(TODO_ItemDTO item)
        {
            try
            {
                var itemToUpdate = _repository.GetTODO_ItemById(item.IdItem);
                if (itemToUpdate == null)
                {
                    return new ServiceResponse<TODO_ItemDTO>
                    {
                        Code = ServiceResponseCode.ErrorBusiness,
                        Message = "l'item n'a pas été retrouvé en base"
                    };
                }

                itemToUpdate.Title = item.Title;
                itemToUpdate.Description = item.Description;
                itemToUpdate.IdStatus = (int)item.todo_ItemsStatus;

                var response = _repository.UpdateTODO_Item(itemToUpdate);

                if (response == null)
                {
                    return new ServiceResponse<TODO_ItemDTO>
                    {
                        Code = ServiceResponseCode.ErrorDB,
                        Message = "l'item n'a pas été mise à jour en base"
                    };
                }

                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.Success,
                    Message = "l'item a été mise à jour en base",
                    Data = item
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Erreur durant la mise à jour du todo item : {0}", ex.Message));
                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.Error,
                    Message = "Erreur durant la mise à jour du todo item"
                };
            }      
        }
    }
}
