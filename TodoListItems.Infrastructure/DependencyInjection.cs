using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoListItems.Domain.Models;
using TodoListItems.Infrastructure.Interfaces;
using TodoListItems.Infrastructure.Repositories;

namespace TodoListItems.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TODO_LISTContext>(options =>
                options.UseSqlServer(TodoListDatabaseSettings.ConnectionString));

            services.AddScoped<ITodoListItemsRepository, TodoListItemsRepository>();

            return services;
        }
    }
}
