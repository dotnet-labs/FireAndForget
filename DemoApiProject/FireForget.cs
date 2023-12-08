namespace DemoApiProject;

public sealed class FireForget(IServiceProvider serviceProvider)
{
    public void Execute<TService>(Func<TService, Task> func) where TService : notnull
    {
        Task.Run(async () =>
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var svc = scope.ServiceProvider.GetRequiredService<TService>();
                await func(svc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }
}