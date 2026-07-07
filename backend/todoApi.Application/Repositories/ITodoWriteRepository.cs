namespace todoApi.Application.Repositories;

using todoApi.Application.Models;

public interface ITodoWriteRepository
{
    Task<Todo> CreateAsync(Todo todo);
    Task<bool> UpdateAsync(int id, Todo input);
    Task<bool> DeleteAsync(int id);
}