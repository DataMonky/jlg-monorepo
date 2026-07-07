namespace todoApi.Infrastructure.Repositories;

using todoApi.Application.Models;
using todoApi.Application.Repositories;
using todoApi.Infrastructure.Data;

public class TodoWriteRepository(TodoDb db) : ITodoWriteRepository
{
    public async Task<Todo> CreateAsync(Todo todo, CancellationToken cancellationToken = default)
    {
        db.Todos.Add(todo);
        await db.SaveChangesAsync(cancellationToken);
        return todo;
    }

    public async Task<bool> UpdateAsync(int id, Todo input, CancellationToken cancellationToken = default)
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo is null) return false;
        todo.Name = input.Name;
        todo.IsComplete = input.IsComplete;
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await db.Todos.FindAsync(id) is Todo todo)
        {
            db.Todos.Remove(todo);
            await db.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}