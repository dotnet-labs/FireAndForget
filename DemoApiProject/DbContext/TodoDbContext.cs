using DemoApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApiProject.DbContext;

public class TodoDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Todo> Todos { get; protected set; } = null!;
    public DbSet<TodoHistory> TodoHistories { get; protected set; } = null!;

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<TodoHistory>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
        });
    }
}