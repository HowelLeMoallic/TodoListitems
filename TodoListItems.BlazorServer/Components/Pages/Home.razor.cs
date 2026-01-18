
using TodoListItems.Application.DTO;
using TodoListItems.BlazorServer.Controllers;
using System.Net.Http;

namespace TodoListItems.BlazorServer.Components.Pages
{
    public partial class Home
    {
        private List<TODO_ItemDTO> todo_ItemDTOs = [];
        private static readonly HttpClient httpClient = new HttpClient();

        protected override async Task OnInitializedAsync()
        {
            var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<TODO_ItemDTO>>>("https://localhost:7004/todolistitems");
            todo_ItemDTOs = response?.Data ?? [];
        }
    }
}