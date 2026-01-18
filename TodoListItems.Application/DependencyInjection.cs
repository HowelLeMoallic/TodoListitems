using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListItems.Application.Interfaces;
using TodoListItems.Application.Services;

namespace TodoListItems.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITodoListItemsService, TodoListItemsService>();
            return services;
        }
    }
}
