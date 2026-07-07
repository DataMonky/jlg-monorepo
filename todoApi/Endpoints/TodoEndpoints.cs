namespace todoApi.Endpoints;

using todoApi.Repositories;

public static class TodoEndpoints
{
    public static WebApplication MapTodoEndpoints(this WebApplication app)
    {
        app.MapGet("/todoitems", async (ITodoRepository repo) => await repo.GetAllAsync());

        app.MapPost("/todoitems", async (Todo todo, ITodoRepository repo) =>
        {
            var createdTodo = await repo.CreateAsync(todo);
            return Results.Created($"/todoitems/{createdTodo.Id}", createdTodo);
        });

        app.MapDelete("/todoitems/{id}", async (int id, ITodoRepository repo) =>
        {
            var result = await repo.DeleteAsync(id);
            if (!result) return Results.NotFound();
            return Results.NoContent();
        });

        app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, ITodoRepository repo) =>
        {
            var result = await repo.UpdateAsync(id, inputTodo);
            if (!result) return Results.NotFound();
            return Results.NoContent();
        });

        return app;
    }
}
