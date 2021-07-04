using DanskeBank.Application.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DanskeBank.CompaniesApi.Diagnostic.Health
{
    public class Readiness : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly StartupHostedServiceHealthCheck _startupHostedServiceHealthCheck;

        private readonly ISsnService _ssnService;

        public Readiness(ILogger<Readiness> logger,
            StartupHostedServiceHealthCheck startupHostedServiceHealthCheck,
            ISsnService ssnService)
        {
            _logger = logger;
            _startupHostedServiceHealthCheck = startupHostedServiceHealthCheck;
            _ssnService = ssnService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("startup hosted service is starting");


            Task.Run(async () =>
            {
                // CHECK IF ALL THE SERVICES ARE RUNNING OK

                // bad example, but OK
                if (!await _ssnService.CheckSSNAsync("123-45-6789"))
                {
                    _logger.LogCritical("ssnService");
                    return;
                }

                _startupHostedServiceHealthCheck.StartupTaskCompleted = true;

                _logger.LogInformation("startup hosted services started");
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("startup hosted services is stopping.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
