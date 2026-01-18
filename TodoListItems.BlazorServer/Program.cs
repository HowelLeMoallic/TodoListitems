using Microsoft.EntityFrameworkCore;
using TodoListItems.Application;
using TodoListItems.BlazorServer.Components;
using TodoListItems.Domain.Models;
using TodoListItems.Infrastructure;
using TodoListItems.Infrastructure.Interfaces;
using TodoListItems.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Ajout du controller
builder.Services.AddControllers();

//Ajout du swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ajout de la connection string depuis le config
var dbConfig = builder.Configuration.GetSection(nameof(TodoListDatabaseSettings));
TodoListDatabaseSettings.ConnectionString = dbConfig.GetValue<string>(nameof(TodoListDatabaseSettings.ConnectionString));

//Injection de dépendances des services et repositories
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Permet de mapper les [Route] et [Http...] des controllers
//Afin de recevoir les requêtes https
app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
