using Microsoft.Extensions.Logging;
using TodoListItems.Domain.Models;
using TodoListItems.Infrastructure.Interfaces;

namespace TodoListItems.Infrastructure.Repositories
{
    public class TodoListItemsRepository : ITodoListItemsRepository
    {
        private readonly TODO_LISTContext _context;
        private readonly ILogger<TodoListItemsRepository> _logger;

        public TodoListItemsRepository(TODO_LISTContext context, ILogger<TodoListItemsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Récupération des to list items depuis la db
        /// </summary>
        /// <returns></returns>
        public List<TODO_Item> GetAllTODO_Items()
        {
            var query = from tdi in _context.TODO_Items
                        join tds in _context.TODO_ItemStatuses on tdi.IdStatus equals tds.IdStatus
                        select new
                        {
                            tdi,
                            tds
                        };


            return query.Select( x => new TODO_Item
            {
                Created = x.tdi.Created,
                CreatedBy = x.tdi.CreatedBy,
                Description = x.tdi.Description,
                IdItem = x.tdi.IdItem,
                IdStatus = x.tdi.IdStatus,
                Title = x.tdi.Title
            }).ToList();
            
        }

        /// <summary>
        /// Récupération d'un todo item avec son id
        /// </summary>
        /// <param name="id">id du todo item</param>
        /// <returns></returns>
        public TODO_Item GetTODO_ItemById(int id)
        {
            var query = from tdi in _context.TODO_Items
                        join tds in _context.TODO_ItemStatuses on tdi.IdStatus equals tds.IdStatus
                        where tdi.IdItem == id
                        select new
                        {
                            tdi,
                            tds
                        };

            return query.Select(x => new TODO_Item
            {
                Created = x.tdi.Created,
                CreatedBy = x.tdi.CreatedBy,
                Description = x.tdi.Description,
                IdItem = x.tdi.IdItem,
                IdStatus = x.tdi.IdStatus,
                Title = x.tdi.Title
            }).First();
        }

        /// <summary>
        /// Mise à jour d'un todo item
        /// </summary>
        /// <param name="item">item à mettre à jour</param>
        /// <returns></returns>
        public TODO_Item UpdateTODO_Item(TODO_Item item)
        {
            TODO_Item itemUpdated =  _context.TODO_Items.Update(item).Entity;
            return itemUpdated;
        }
    }
}
