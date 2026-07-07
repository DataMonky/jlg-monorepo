using MediatR;
using todoApi.Infrastructure;
using todoApi.Endpoints;
using todoApi.Application.Commands;
using todoApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg =>
      cfg.RegisterServicesFromAssembly(typeof(CreateTodoCommand).Assembly));
builder.Services.AddInfrastructure();
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

// Register essential problem details and your basic handler
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Must be at the very top of your middleware pipeline
app.UseExceptionHandler(); 

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

app.MapTodoEndpoints();

app.Run();