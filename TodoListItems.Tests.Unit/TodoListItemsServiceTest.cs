using Microsoft.Extensions.Logging;
using Moq;
using TodoListItems.Application.DTO;
using TodoListItems.Application.Enum;
using TodoListItems.Application.Services;
using TodoListItems.Domain.Models;
using TodoListItems.Infrastructure.Interfaces;
using TodoListItems.Infrastructure.Repositories;

namespace TodoListItems.Tests.Unit
{
    public class TodoListItemsServiceTest
    {
        [Fact]
        public void GetAllTODO_ItemsTest()
        {
            // Arrange
            var mockRepo = new Mock<ITodoListItemsRepository>();

            var fakeTodos = new List<TODO_Item>
            {
                new TODO_Item()
                {
                    IdItem = 1,
                    Title = "Test 1",
                    IdStatus = (int)TODO_ItemsStatus.ToDo,
                    Created = DateTime.Now,
                    CreatedBy = 1,
                    Description = "Description",
                },
                new TODO_Item()
                {
                    IdItem = 2,
                    Title = "Test 2",
                    IdStatus = (int)TODO_ItemsStatus.InProgress,
                    Created = DateTime.Now,
                    CreatedBy = 1,
                    Description = "Description",
                },
                new TODO_Item()
                {
                    IdItem = 3,
                    Title = "Test 3",
                    IdStatus = (int)TODO_ItemsStatus.Finished,
                    Created = DateTime.Now,
                    CreatedBy = 1,
                    Description = "Description",
                },

            };

            mockRepo
                .Setup(r => r.GetAllTODO_Items())
                .Returns(fakeTodos);
            var mockILogger = new Mock<ILogger<TodoListItemsRepository>>();
            var service = new TodoListItemsService(mockRepo.Object, mockILogger.Object);

            //Act
            ServiceResponse<List<TODO_ItemDTO>> response = service.GetAllTODO_Items();

            //Assert
            Assert.NotNull(response.Data);
        }

        [Fact]
        public void GetFilterTODO_Items()
        {
            // Arrange
            var mockRepo = new Mock<ITodoListItemsRepository>();

            var fakeTodos = new List<TODO_Item>
            {
                new TODO_Item()
                {
                    IdItem = 1,
                    Title = "Test 1",
                    IdStatus = (int)TODO_ItemsStatus.ToDo,
                    Created = DateTime.Now,
                    CreatedBy = 1,
                    Description = "Description",
                },
                new TODO_Item()
                {
                    IdItem = 2,
                    Title = "Test 2",
                    IdStatus = (int)TODO_ItemsStatus.InProgress,
                    Created = DateTime.Now,
                    CreatedBy = 1,
                    Description = "Description",
                },
                new TODO_Item()
                {
                    IdItem = 3,
                    Title = "Test 3",
                    IdStatus = (int)TODO_ItemsStatus.Finished,
                    Created = DateTime.Now,
                    CreatedBy = 1,
                    Description = "Description",
                },

            };

            mockRepo
                .Setup(r => r.GetAllTODO_Items())
                .Returns(fakeTodos);

            var mockILogger = new Mock<ILogger<TodoListItemsRepository>>();
            var service = new TodoListItemsService(mockRepo.Object, mockILogger.Object);
            FliterTodoListItem filter = new FliterTodoListItem()
            {
                IdItemStatus = 1
            };
            //Act
            ServiceResponse<List<TODO_ItemDTO>> response = service.GetFilterTODO_Items(filter);

            //Assert
            Assert.True(response.IsSuccess);
            if (response.Data!.Any())
            {
                Assert.All(response.Data!,
                item => Assert.Equal(TODO_ItemsStatus.ToDo, item.todo_ItemsStatus));
            }

        }
    }
}
