using HxcCommon;
using Microsoft.EntityFrameworkCore;

namespace HxcApiJit.Ef;

public class HxcDbContext(DbContextOptions<HxcDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .Property(t => t.DueBy)
            .HasConversion(
                v => v.HasValue ? new DateTime(v.Value.Year, v.Value.Month, v.Value.Day) : (DateTime?)null,
                v => v.HasValue ? DateOnly.FromDateTime(v.Value) : null
            );

        modelBuilder.Entity<Todo>().HasData(
            new Todo(1, "Loaded from db 1"),
            new Todo(2, "Loaded from db 2")
        );
    }
}