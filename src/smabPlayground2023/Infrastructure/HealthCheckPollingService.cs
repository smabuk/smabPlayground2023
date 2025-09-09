// Pseudocode:
// 1. Define sealed class HealthCheckPollingService implementing IHostedService and IDisposable.
// 2. Inject: HealthCheckService (runs registered health checks), ILogger, IServiceProvider (optional future use).
// 3. On StartAsync:
//    - Initialize a System.Threading.Timer with dueTime = 0 and period = 13 minutes.
//    - Timer callback calls an async method PollAsync safely (no overlapping executions).
// 4. PollAsync:
//    - Use an async lock (SemaphoreSlim) to prevent re-entrancy.
//    - Execute healthService.CheckHealthAsync().
//    - Log overall status and any unhealthy/degraded entries.
// 5. On StopAsync: stop timer (Change to Timeout.Infinite).
// 6. Dispose: dispose timer and semaphore.
// 7. Register this service in Program.cs (builder.Services.AddHostedService<HealthCheckPollingService>();).
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace smabPlayground2023.Infrastructure;

public sealed class HealthCheckPollingService(
	HealthCheckService healthCheckService,
	ILogger<HealthCheckPollingService> logger) : IHostedService, IDisposable
{
	private Timer? _timer;
	private readonly SemaphoreSlim _semaphore = new(1, 1);
	private static readonly TimeSpan Interval = TimeSpan.FromMinutes(13);

	public Task StartAsync(CancellationToken cancellationToken)
	{
		logger.LogDebug("HealthCheckPollingService starting. Interval: {Interval}", Interval);
		_timer = new Timer(static s => _ = ((HealthCheckPollingService)s!).SafePollAsync(), this, TimeSpan.Zero, Interval);
		return Task.CompletedTask;
	}

	private async Task SafePollAsync()
	{
		if (!await _semaphore.WaitAsync(0)) {
			return; // Skip if previous run still active.
		}

		try {
			await PollAsync();
		}
		catch (Exception ex) {
			logger.LogError(ex, "Unhandled exception during health check polling.");
		}
		finally {
			_ = _semaphore.Release();
		}
	}

	private async Task PollAsync()
	{
		var report = await healthCheckService.CheckHealthAsync();
		if (report.Status == HealthStatus.Healthy) {
			logger.LogDebug("Health check OK. Status: {Status}", report.Status);
			return;
		}

		logger.LogWarning("Health check status: {Status}", report.Status);

		foreach (var entry in report.Entries) {
			if (entry.Value.Status != HealthStatus.Healthy) {
				logger.LogWarning(
					" - {Key}: {Status}. Description: {Description}. Error: {Error}",
					entry.Key,
					entry.Value.Status,
					entry.Value.Description,
					entry.Value.Exception?.Message);
			}
		}
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("HealthCheckPollingService stopping.");
		_ = _timer?.Change(Timeout.Infinite, 0);
		return Task.CompletedTask;
	}

	public void Dispose()
	{
		_timer?.Dispose();
		_semaphore.Dispose();
	}
}
