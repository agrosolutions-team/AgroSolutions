using AgroSolutions.IoT.IngestaoDados.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgroSolutions.IoT.IngestaoDados.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<LeituraSensorTalhao> LeiturasSensorTalhao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
