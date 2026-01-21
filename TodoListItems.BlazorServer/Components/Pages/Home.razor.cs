
using Microsoft.AspNetCore.Components;
using TodoListItems.Application.DTO;

namespace TodoListItems.BlazorServer.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private List<TODO_ItemDTO> todo_ItemDTOs = [];
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7004/"),
        };

        protected override async Task OnInitializedAsync()
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
            var response = await httpClient.PostAsJsonAsync<FliterTodoListItem>("https://localhost:7004/todolistitems", filter);

            //Transforme retour pour mettre à jour la liste
            var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<List<TODO_ItemDTO>>>();
            todo_ItemDTOs = serviceResponse?.Data ?? [];
        }

        /// <summary>
        /// Permet de changer de page
        /// </summary>
        /// <param name="uri">url de la page</param>
        public void Navigate(string uri)
        {
            NavigationManager.NavigateTo(uri, false, false);
        }

        private void GoToCreate() => Navigate("/Create");
        private void GoToEdit(int id) => Navigate(String.Format("/Edit/{0}",id));
    }
}