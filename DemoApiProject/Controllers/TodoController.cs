using DemoApiProject.DbContext;
using DemoApiProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;
        private readonly FireForget _fireForget;

        public TodoController(TodoDbContext dbContext, FireForget fireForget)
        {
            _dbContext = dbContext;
            _fireForget = fireForget;
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Create(Todo request)
        {
            var todo = new Todo
            {
                Description = request.Description
            };

            await _dbContext.AddAsync(todo);
            await _dbContext.SaveChangesAsync();

            // insert TodoHistory in background
            _fireForget.Execute<TodoDbContext>(async ctx =>
            {
                var history = new TodoHistory
                {
                    TodoId = todo.Id,
                    Todo = todo,
                    Date = DateTime.Now,
                    Action = "Created"
                };

                await ctx.AddAsync(history);
                await ctx.SaveChangesAsync();

                // dispose the TodoContext
                await ctx.DisposeAsync();
            });

            return Ok(todo);
        }

        [HttpGet("histories")]
        public async Task<ActionResult> ViewAllHistories()
        {
            var result = await _dbContext.TodoHistories.AsNoTracking().ToListAsync();
            return Ok(result);
        }
    }
}