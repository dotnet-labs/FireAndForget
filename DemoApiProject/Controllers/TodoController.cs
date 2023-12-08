using DemoApiProject.DbContext;
using DemoApiProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController(TodoDbContext dbContext, FireForget fireForget) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Todo>> Create([FromBody] string description )
        {
            var todo = new Todo
            {
                Description = description
            };

            await dbContext.AddAsync(todo);
            await dbContext.SaveChangesAsync();

            // insert TodoHistory in background
            fireForget.Execute<TodoDbContext>(async ctx =>
            {
                Console.WriteLine("begin background task");
                var history = new TodoHistory
                {
                    TodoId = todo.Id,
                    Date = DateTime.Now,
                    Action = "Created"
                };

                await ctx.AddAsync(history);
                await ctx.SaveChangesAsync();

                Console.WriteLine("end background task");
                //// dispose the TodoContext
                //await ctx.DisposeAsync();
            });

            return Ok(todo);
        }

        [HttpGet("histories")]
        public async Task<ActionResult> ViewAllHistories()
        {
            var result = await dbContext.TodoHistories.AsNoTracking().ToListAsync();
            return Ok(result);
        }
    }
}