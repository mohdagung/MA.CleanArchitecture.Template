using MA.Clean.Template.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructure();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

public sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
