namespace todoApi.Application.Commands;

using MediatR;
using todoApi.Application.Repositories;

public record DeleteTodoCommand(int Id) : IRequest<bool>;

public class DeleteTodoHandler(ITodoWriteRepository repo)
    : IRequestHandler<DeleteTodoCommand, bool>
{
    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        return await repo.DeleteAsync(request.Id);
    }
}