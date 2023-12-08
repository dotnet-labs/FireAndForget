using DemoApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApiProject.DbContext;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Todo> Todos { get; protected set; } = null!;
    public DbSet<TodoHistory> TodoHistories { get; protected set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.Property(x => x.Description).HasMaxLength(100);
        });
        modelBuilder.Entity<TodoHistory>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.Property(x => x.Action).HasMaxLength(10);
        });
    }
}