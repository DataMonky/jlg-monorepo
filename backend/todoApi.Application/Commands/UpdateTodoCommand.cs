namespace todoApi.Application.Commands;

using MediatR;
using todoApi.Application.Repositories;

public record UpdateTodoCommand(int Id, string? Name, bool IsComplete) : IRequest<bool>;

public class UpdateTodoHandler(ITodoWriteRepository repo)
    : IRequestHandler<UpdateTodoCommand, bool>
{
    public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        return await repo.UpdateAsync(request.Id, new Models.Todo
        {
            Name = request.Name,
            IsComplete = request.IsComplete
        });
    }
}