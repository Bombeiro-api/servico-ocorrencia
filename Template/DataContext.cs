using Microsoft.EntityFrameworkCore;
using Ocorrencias.DTO;

namespace Ocorrencias;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Ocorrencia> Ocorrencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ocorrencia>()
            .HasKey(x => x.Id);

        base.OnModelCreating(modelBuilder);
    }
}