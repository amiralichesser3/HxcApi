using HxcCommon;
using Microsoft.EntityFrameworkCore;

namespace HxcApiJit.Ef;

public class HxcWriteDbContext(DbContextOptions<HxcWriteDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<ErrorLogEvent> ErrorLogEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().HasData(
            new Todo(1, "Loaded from db 1"),
            new Todo(2, "Loaded from db 2")
        );
    }
}