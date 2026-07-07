namespace todoApi.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using todoApi.Application.Repositories;
using todoApi.Infrastructure.Data;
using todoApi.Infrastructure.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
        services.AddScoped<ITodoReadRepository, TodoReadRepository>();
        services.AddScoped<ITodoWriteRepository, TodoWriteRepository>();
        return services;
    }
}
