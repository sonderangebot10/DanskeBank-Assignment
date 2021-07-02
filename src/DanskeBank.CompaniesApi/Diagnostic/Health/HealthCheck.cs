﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace DanskeBank.CompaniesApi.Diagnostic.Health
{
    public class HealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(HealthCheckResult.Healthy("GOOD"));
        }
    }
}
