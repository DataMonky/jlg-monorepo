namespace todoApi.Application.Repositories;

using todoApi.Application.Models;

public interface ITodoWriteRepository
{
    Task<Todo> CreateAsync(Todo todo, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(int id, Todo input, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}