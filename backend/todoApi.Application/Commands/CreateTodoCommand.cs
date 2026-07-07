namespace todoApi.Application.Commands;

using MediatR;
using todoApi.Application.Models;
using todoApi.Application.Repositories;

public record CreateTodoCommand(string? Name, bool IsComplete) : IRequest<Todo>;

public class CreateTodoHandler(ITodoWriteRepository repo)
    : IRequestHandler<CreateTodoCommand, Todo>
{
    public async Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo { Name = request.Name, IsComplete = request.IsComplete };
        return await repo.CreateAsync(todo);
    }
}