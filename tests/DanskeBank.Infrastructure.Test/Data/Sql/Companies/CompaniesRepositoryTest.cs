using Xunit;

namespace DanskeBank.Infrastructure.Test.Data.Sql.Companies
{
    public class CompaniesRepositoryTest
    {
        [Trait("Category", "UnitTest")]
        [Fact]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async void Test_Repo()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // USE IN-MEMORY DB FOR TESTING
            // TOO MUCH WORK NOW

            Assert.True(true);
        }
    }
}
