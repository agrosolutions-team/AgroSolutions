using AgroSolutions.IoT.IngestaoDados.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(DataContext context)
    {
        await context.Database.MigrateAsync();
    }
}
