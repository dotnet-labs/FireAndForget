namespace DemoApiProject;

public sealed class FireForget
{
    private readonly IServiceProvider _serviceProvider;

    public FireForget(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Execute<TService>(Func<TService, Task> func) where TService : notnull
    {
        Task.Run(async () =>
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
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