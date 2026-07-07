namespace todoApi.Configuration;

public static class DataSeeder
{
    public static WebApplication SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TodoDb>();
        if (!db.Todos.Any())
        {
            db.Todos.AddRange(
                new Todo { Name = "prepare cv", IsComplete = true },
                new Todo { Name = "submit video interview", IsComplete = true },
                new Todo { Name = "technical assessment", IsComplete = false }
            );
            db.SaveChanges();
        }
        return app;
    }
}
