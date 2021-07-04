using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Autofac.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using DanskeBank.CompaniesApi.Api.Companies;
using Microsoft.Extensions.Logging;
using DanskeBank.CompaniesApi.Services;
using Moq;

namespace DanskeBank.CompaniesApi.IntegartionTest
{
    public class SsnTest
    {
        private const string validSsn = "123-45-6789";

        private string ValidateSsnUrl(string ssn) => $"/api/ssn/{ssn}/Validate";

        // requires services for doctrin server setting to run
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SsnTest()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var env = builderContext.HostingEnvironment;
                    config.AddJsonFile("autofac.json")
                    .AddEnvironmentVariables();
                })
                .ConfigureServices(services => services.AddAutofac());

            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

        [Trait("Category", "IntegrationTest")]
        [Fact]
        public async Task SsnController_Unauthorized()
        {
            var result = await _client.GetAsync(ValidateSsnUrl(validSsn));

            Assert.False(result.IsSuccessStatusCode);
        }

        [Trait("Category", "IntegrationTest")]
        [Fact]
        public async Task SsnController_Authorized()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "admin");

            var result = await _client.GetAsync(ValidateSsnUrl(validSsn));

            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
