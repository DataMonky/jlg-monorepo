namespace todoApi.Application.Queries;

using MediatR;
using todoApi.Application.Models;
using todoApi.Application.Repositories;

public record GetAllTodosQuery : IRequest<List<Todo>>;

public class GetAllTodosHandler(ITodoReadRepository repo)
    : IRequestHandler<GetAllTodosQuery, List<Todo>>
{
    public async Task<List<Todo>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        return await repo.GetAllAsync(cancellationToken);
    }
}