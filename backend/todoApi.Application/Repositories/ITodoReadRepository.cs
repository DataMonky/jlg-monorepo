namespace todoApi.Application.Repositories;

using todoApi.Application.Models;

public interface ITodoReadRepository
{
    Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
}