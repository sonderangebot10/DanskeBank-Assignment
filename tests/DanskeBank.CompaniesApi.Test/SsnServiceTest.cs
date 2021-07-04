using DanskeBank.CompaniesApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DanskeBank.CompaniesApi.Test
{
    public class SsnServiceTest
    {
        private const string validSsn = "123-45-6789";
        private const string invalidSsn = "123-45";

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void SsnService_SsnValidator_Valid()
        {
            var mockLogger = new Mock<ILogger<SsnService>>();
            var ssnService = new SsnService(mockLogger.Object);

            var result = await ssnService.CheckSSNAsync(validSsn);

            Assert.True(result);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void SsnService_SsnValidator_Invalid()
        {
            var mockLogger = new Mock<ILogger<SsnService>>();
            var ssnService = new SsnService(mockLogger.Object);

            var result = await ssnService.CheckSSNAsync(invalidSsn);

            Assert.False(result);
        }
    }
}
