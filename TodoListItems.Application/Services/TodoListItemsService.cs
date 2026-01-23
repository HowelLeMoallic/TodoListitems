using Azure;
using Microsoft.Extensions.Logging;
using TodoListItems.Application.DTO;
using TodoListItems.Application.Enum;
using TodoListItems.Application.Interfaces;
using TodoListItems.Domain.Models;
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
        /// Création d'un item en base
        /// </summary>
        /// <param name="item">item à créer</param>
        /// <returns></returns>
        public ServiceResponse<TODO_ItemDTO> CreateTODO_Item(TODO_ItemDTO item)
        {
            if (item == null)
            {
                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.ErrorBusiness,
                    Message = String.Format("Un item null ne peut pas être créé")
                };
            }

            if (String.IsNullOrEmpty(item.Title))
            {
                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.ErrorBusiness,
                    Message = String.Format("Un item doit avoir un titre")
                };
            }

            if (String.IsNullOrEmpty(item.Description))
            {
                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.ErrorBusiness,
                    Message = String.Format("Un item doit avoir une description")
                };
            }

            if ((int)item.todo_ItemsStatus == 0)
            {
                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.ErrorBusiness,
                    Message = String.Format("Un item doit avoir un statut")
                };
            }

            if (item.CreatedBy == 0)
            {
                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.ErrorBusiness,
                    Message = String.Format("Un item doit être associé à un utilisateur")
                };
            }

            TODO_Item itemToCreate  = new TODO_Item()
            {
                CreatedBy = item.CreatedBy,
                Created = item.Created,
                Description = item.Description,
                IdItem = item.IdItem,
                IdStatus = (int)item.todo_ItemsStatus,
                Title = item.Title
            };

            var itemCreated = _repository.CreateTODO_Item(itemToCreate);

            if (itemCreated != null)
            {
                return new ServiceResponse<TODO_ItemDTO>
                {
                    Code = ServiceResponseCode.ErrorDB,
                    Message = String.Format("Une erreur est survenue dans l'insertion de l'item en base")
                };
            }

            item.IdItem = itemCreated!.IdItem;
            return new ServiceResponse<TODO_ItemDTO>
            {
                Code = ServiceResponseCode.Success,
                Message = String.Format("Un item doit avoir un titre"),
                Data = item
            };

        }

        /// <summary>
        /// Suppression d'un item dans la todo list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<bool> DeleteTODO_Item(int id)
        {
            try
            {
                bool response = _repository.DeleteTODO_Item(id);

                if (!response)
                {
                    return new ServiceResponse<bool>
                    {
                        Code = ServiceResponseCode.ErrorDB,
                        Message = String.Format("Erreur durant la suppression de l'item dans la todo list"),
                        Data = false
                    };
                }

                return new ServiceResponse<bool>
                {
                    Code = ServiceResponseCode.Success,
                    Message = "Succès",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("Erreur durant la récupération des todo list items : {0}", ex.Message));
                return new ServiceResponse<bool>
                {
                    Code = ServiceResponseCode.Error,
                    Message = String.Format("Erreur durant la suppression de l'item dans la todo list"),
                    Data= false
                };
            }
            
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
                    Message = String.Format("Erreur durant la récupération des todo list items ")
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
                    Message = String.Format("Erreur durant la récupération des todo list items filtrés ")
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
