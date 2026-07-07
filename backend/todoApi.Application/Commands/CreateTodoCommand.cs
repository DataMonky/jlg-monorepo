namespace todoApi.Application.Commands;

using MediatR;
using todoApi.Application.Models;
using todoApi.Application.Repositories;

public record CreateTodoCommand(string? Name, bool IsComplete) : IRequest<Todo>;

public class CreateTodoHandler(
    ITodoWriteRepository writeRepo,
    ITodoReadRepository readRepo)
    : IRequestHandler<CreateTodoCommand, Todo>
{
    public async Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var existing = await readRepo.GetAllAsync();
        if (existing.Any(t => string.Equals(t.Name, request.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("A todo with this name already exists.");
        }

        var todo = new Todo { Name = request.Name, IsComplete = request.IsComplete };
        return await writeRepo.CreateAsync(todo);
    }
}