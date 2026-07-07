namespace todoApi.Endpoints;

using todoApi.Application.Models;
using todoApi.Application.Repositories;

public static class TodoEndpoints
{
    public static WebApplication MapTodoEndpoints(this WebApplication app)
    {
        app.MapGet("/todoitems", async (ITodoReadRepository repo) => await repo.GetAllAsync());

        app.MapPost("/todoitems", async (Todo todo, ITodoWriteRepository repo) =>
        {
            var createdTodo = await repo.CreateAsync(todo);
            return Results.Created($"/todoitems/{createdTodo.Id}", createdTodo);
        });

        app.MapDelete("/todoitems/{id}", async (int id, ITodoWriteRepository repo) =>
        {
            var result = await repo.DeleteAsync(id);
            if (!result) return Results.NotFound();
            return Results.NoContent();
        });

        app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, ITodoWriteRepository repo) =>
        {
            var result = await repo.UpdateAsync(id, inputTodo);
            if (!result) return Results.NotFound();
            return Results.NoContent();
        });

        return app;
    }
}
