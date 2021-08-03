using DanskeBank.Application.Services;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DanskeBank.CompaniesApi.Services
{
    public class SsnService : ISsnService
    {
        private readonly ILogger<SsnService> _logger;

        private const string RegexSsnPattern = @"^(\d{3}-?\d{2}-?\d{4})$";

        public SsnService(ILogger<SsnService> logger)
        {
            _logger = logger;
        }

        public Task<bool> CheckSSNAsync(string ssn)
        {
            _logger.LogInformation($"checking {ssn} validity.");

            Regex rg = new Regex(RegexSsnPattern, RegexOptions.IgnoreCase);
            var result = rg.IsMatch(ssn);

            _logger.LogInformation($"{ssn} is {result}.");

            return Task.FromResult(result);
        }
    }
}
