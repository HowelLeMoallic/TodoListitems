using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using TodoListItems.Application.DTO;

namespace TodoListItems.BlazorServer.Components.Pages
{
    public partial class CreateToDoListItem
    {
        public TODO_ItemDTO ItemDto { get; set; } = new TODO_ItemDTO();
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7004/"),
        };
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private async Task SubmitForm()
        {
            ItemDto.Created = DateTime.Now;
            ItemDto.CreatedBy = 1;

            var response = await httpClient.PostAsJsonAsync<TODO_ItemDTO>("todolistitems/create", ItemDto);
            if(response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/", false, false);
            }
        }
    }
}