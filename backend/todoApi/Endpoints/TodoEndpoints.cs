namespace todoApi.Endpoints;

using todoApi.Application.Commands;
using todoApi.Application.Queries;
using todoApi.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public static class TodoEndpoints
{
  public static WebApplication MapTodoEndpoints(this WebApplication app)
  {
    app.MapGet("/todoitems", async ([FromServices] IMediator mediator) =>
  await mediator.Send(new GetAllTodosQuery()));

    app.MapPost("/todoitems", async (CreateTodoCommand command, [FromServices] IMediator mediator) =>
    {
      if (string.IsNullOrWhiteSpace(command.Name))
      {
        return Results.BadRequest("Todo name is required.");
      }

      var todo = await mediator.Send(command);
      return Results.Created($"/todoitems/{todo.Id}", todo);
    });

    app.MapDelete("/todoitems/{id}", async (int id, [FromServices] IMediator mediator) =>
  await mediator.Send(new DeleteTodoCommand(id))
      ? Results.NoContent()
      : Results.NotFound());

    app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, [FromServices] IMediator mediator) =>
  await mediator.Send(new UpdateTodoCommand(id, inputTodo.Name, inputTodo.IsComplete))
      ? Results.NoContent()
      : Results.NotFound());

    return app;
  }
}
