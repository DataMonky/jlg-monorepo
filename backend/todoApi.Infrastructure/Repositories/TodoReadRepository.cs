namespace todoApi.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using todoApi.Application.Models;
using todoApi.Application.Repositories;
using todoApi.Infrastructure.Data;

public class TodoReadRepository(TodoDb db) : ITodoReadRepository
{
    public async Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await db.Todos.AsNoTracking().ToListAsync(cancellationToken);
}