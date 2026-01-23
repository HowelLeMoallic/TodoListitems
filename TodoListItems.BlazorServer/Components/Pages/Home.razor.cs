
using Microsoft.AspNetCore.Components;
using TodoListItems.Application.DTO;
using TodoListItems.Application.Enum;

namespace TodoListItems.BlazorServer.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<TODO_ItemDTO> todo_ItemDTOs = [];
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri(Config.Config.Url),
        };

        protected override async Task OnInitializedAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<TODO_ItemDTO>>>("todolistitems");
            todo_ItemDTOs = response?.Data ?? [];
        }

        /// <summary>
        /// Permet de reset la liste des items
        /// </summary>
        /// <returns></returns>
        public async Task RefreshDatas()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<TODO_ItemDTO>>>("todolistitems");
            todo_ItemDTOs = response?.Data ?? [];
        }

        /// <summary>
        /// Permet de filtrer la liste des items de la todo list en fonction de leur état
        /// </summary>
        /// <param name="idItemStatus">id status</param>
        /// <returns></returns>
        public async Task FilterToDoListItems(int idItemStatus)
        {
            //Création d'un filtre avec l'id
            FliterTodoListItem filter = new FliterTodoListItem()
            {
                IdItemStatus = idItemStatus
            };
            //Envoie requête au controller
            var response = await httpClient.PostAsJsonAsync<FliterTodoListItem>("todolistitems", filter);

            //Transforme retour pour mettre à jour la liste
            var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<List<TODO_ItemDTO>>>();
            todo_ItemDTOs = serviceResponse?.Data ?? [];
        }

        /// <summary>
        /// Met à jour le statut
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task UpdateStatus(TODO_ItemDTO item)
        {
            item.todo_ItemsStatus = TODO_ItemsStatus.Finished;
            var response = await httpClient.PutAsJsonAsync<TODO_ItemDTO>("todolistitems", item);

            var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<TODO_ItemDTO>>();

        }

        /// <summary>
        /// Supprime l'item en db
        /// </summary>
        /// <param name="idItemStatus"></param>
        /// <returns></returns>
        public async Task DeleteToDoItem(int idItemStatus)
        {
            var response = await httpClient.DeleteFromJsonAsync<ServiceResponse<Boolean>>("todolistitems/"+ idItemStatus.ToString());

            if(response?.IsSuccess == true)
            {
                todo_ItemDTOs.Remove(todo_ItemDTOs.First(item => item.IdItem == idItemStatus));
            }
            
        }


        /// <summary>
        /// Permet de changer de page
        /// </summary>
        /// <param name="uri">url de la page</param>
        public void Navigate(string uri)
        {
            NavigationManager.NavigateTo(uri, false, false);
        }

        #region Changement de page

        /// <summary>
        /// Va à la page de création
        /// </summary>
        private void GoToCreate() => Navigate("/Create");

        /// <summary>
        /// Va à la page de modification
        /// </summary>
        /// <param name="id"></param>
        private void GoToEdit(int id) => Navigate(String.Format("/Edit/{0}",id));
        #endregion
    }
}