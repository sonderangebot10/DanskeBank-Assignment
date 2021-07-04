using DanskeBank.Domain.Companies;
using Xunit;

namespace DanskeBank.Domain.Test.Companies
{
    public class OwnerTest
    {
        public class CompanyTest
        {
            [Trait("Category", "UnitTest")]
            [Fact]
            public void Company_ctor()
            {
                var o = new Owner("test", "test");

                Assert.NotNull(o);
            }

            [Trait("Category", "UnitTest")]
            [Fact]
            public void Phrase_ctor_Overload()
            {
                var o = new Owner();

                Assert.NotNull(o);
            }
        }
    }
}
