using DemoApiProject.DbContext;
using DemoApiProject;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseSqlite("Data Source=todo.db"));

builder.Services.AddSingleton<FireForget>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var todoDbContext = app.Services.CreateScope().ServiceProvider.GetService<TodoDbContext>();
todoDbContext?.Database.EnsureCreated();

app.Run();
