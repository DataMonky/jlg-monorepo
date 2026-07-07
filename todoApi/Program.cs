using Microsoft.EntityFrameworkCore;
using todoApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCors(options =>                                                                                                     
      options.AddDefaultPolicy(policy =>                                                 
          policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())                                         
  {
      var db = scope.ServiceProvider.GetRequiredService<TodoDb>();
      if (!db.Todos.Any())                                                                                                                
      {
          db.Todos.AddRange(                                                                                                              
              new Todo { Name = "prepare cv", IsComplete = true },                       
              new Todo { Name = "submit video interview", IsComplete = true },                                                            
              new Todo { Name = "technical assessment", IsComplete = false }
          );                                                                                                                              
          db.SaveChanges();                                                              
      }                                                                                                                                   
  }

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

app.Run();