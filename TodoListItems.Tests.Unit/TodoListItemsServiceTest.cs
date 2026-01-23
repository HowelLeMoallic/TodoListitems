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
        private Mock<ITodoListItemsRepository> mockRepo = new Mock<ITodoListItemsRepository>();
        private List<TODO_Item> fakeTodos = new List<TODO_Item>();

        public TodoListItemsServiceTest() {
            fakeTodos = new List<TODO_Item>
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
        }

        [Fact]
        public void GetAllTODO_ItemsTest()
        {
            //// Arrange
            //var mockRepo = new Mock<ITodoListItemsRepository>();

            //var fakeTodos = new List<TODO_Item>
            //{
            //    new TODO_Item()
            //    {
            //        IdItem = 1,
            //        Title = "Test 1",
            //        IdStatus = (int)TODO_ItemsStatus.ToDo,
            //        Created = DateTime.Now,
            //        CreatedBy = 1,
            //        Description = "Description",
            //    },
            //    new TODO_Item()
            //    {
            //        IdItem = 2,
            //        Title = "Test 2",
            //        IdStatus = (int)TODO_ItemsStatus.InProgress,
            //        Created = DateTime.Now,
            //        CreatedBy = 1,
            //        Description = "Description",
            //    },
            //    new TODO_Item()
            //    {
            //        IdItem = 3,
            //        Title = "Test 3",
            //        IdStatus = (int)TODO_ItemsStatus.Finished,
            //        Created = DateTime.Now,
            //        CreatedBy = 1,
            //        Description = "Description",
            //    },

            //};

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
            //// Arrange
            //var mockRepo = new Mock<ITodoListItemsRepository>();

            //var fakeTodos = new List<TODO_Item>
            //{
            //    new TODO_Item()
            //    {
            //        IdItem = 1,
            //        Title = "Test 1",
            //        IdStatus = (int)TODO_ItemsStatus.ToDo,
            //        Created = DateTime.Now,
            //        CreatedBy = 1,
            //        Description = "Description",
            //    },
            //    new TODO_Item()
            //    {
            //        IdItem = 2,
            //        Title = "Test 2",
            //        IdStatus = (int)TODO_ItemsStatus.InProgress,
            //        Created = DateTime.Now,
            //        CreatedBy = 1,
            //        Description = "Description",
            //    },
            //    new TODO_Item()
            //    {
            //        IdItem = 3,
            //        Title = "Test 3",
            //        IdStatus = (int)TODO_ItemsStatus.Finished,
            //        Created = DateTime.Now,
            //        CreatedBy = 1,
            //        Description = "Description",
            //    },

            //};

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

        [Fact]
        public void DeleteTODOItem()
        {
            int id = 1;
            mockRepo
                .Setup(r => r.DeleteTODO_Item(id))
                .Returns(true);

            var mockILogger = new Mock<ILogger<TodoListItemsRepository>>();
            var service = new TodoListItemsService(mockRepo.Object, mockILogger.Object);

            //Act
            ServiceResponse<bool> response = service.DeleteTODO_Item(id);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public void UpdateTODOItem()
        {
            TODO_Item returnItem = new TODO_Item()
            {
                IdItem = 2,
                Title = "Test de maj de 2",
                IdStatus = (int)TODO_ItemsStatus.Finished,
                Created = DateTime.Now,
                CreatedBy = 1,
                Description = "Description maj",
            };

            TODO_ItemDTO newItem = new TODO_ItemDTO()
            {
                IdItem = 2,
                Title = "Test de maj de 2",
                todo_ItemsStatus = TODO_ItemsStatus.Finished,
                Created = DateTime.Now,
                CreatedBy = 1,
                Description = "Description maj",
            };

            mockRepo
                .Setup(r => r.GetTODO_ItemById(newItem.IdItem))
                .Returns(returnItem);

            mockRepo
                .Setup(r => r.UpdateTODO_Item(returnItem))
                .Returns(returnItem);

            var mockILogger = new Mock<ILogger<TodoListItemsRepository>>();
            var service = new TodoListItemsService(mockRepo.Object, mockILogger.Object);

            
            //Act
            ServiceResponse<TODO_ItemDTO> response = service.UpdateTODO_Item(newItem);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public void CreateTODOItem()
        {
            TODO_Item newItem = new TODO_Item()
            {
                IdItem = 0,
                Title = "Test création item",
                IdStatus = (int)TODO_ItemsStatus.Finished,
                Created = DateTime.Now,
                CreatedBy = 1,
                Description = "Description création item",
            };

            TODO_Item newItemCreated = new TODO_Item()
            {
                IdItem = 4,
                Title = "Test création item",
                IdStatus = (int)TODO_ItemsStatus.Finished,
                Created = DateTime.Now,
                CreatedBy = 1,
                Description = "Description création item",
            };

            TODO_ItemDTO newItemDto = new TODO_ItemDTO()
            {
                IdItem = 0,
                Title = "Test création item",
                todo_ItemsStatus = TODO_ItemsStatus.Finished,
                Created = DateTime.Now,
                CreatedBy = 1,
                Description = "Description création item",
            };

            mockRepo
                .Setup(r => r.CreateTODO_Item(newItem))
                .Returns(newItem);

            var mockILogger = new Mock<ILogger<TodoListItemsRepository>>();
            var service = new TodoListItemsService(mockRepo.Object, mockILogger.Object);

            //Act
            ServiceResponse<TODO_ItemDTO> response = service.CreateTODO_Item(newItemDto);
            Assert.True(response.IsSuccess);
        }
    }
}
