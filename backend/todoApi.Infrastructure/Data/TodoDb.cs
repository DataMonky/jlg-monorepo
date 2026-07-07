namespace todoApi.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using todoApi.Application.Models;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}