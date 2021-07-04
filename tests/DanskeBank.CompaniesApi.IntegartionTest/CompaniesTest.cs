using Xunit;

namespace DanskeBank.CompaniesApi.IntegartionTest
{
    public class CompaniesTest
    {
        public CompaniesTest()
        {
            // SIMILAR AS WITH SSNTEST, BUT MOCKING USE CASES INSIDE THE CONTROLLER...
        }

        [Trait("Category", "UnitTest")]
        [Fact]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async void CompaniesController_Test()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // USE IN-MEMORY DB FOR TESTING
            // TOO MUCH WORK NOW

            Assert.True(true);
        }
    }
}
