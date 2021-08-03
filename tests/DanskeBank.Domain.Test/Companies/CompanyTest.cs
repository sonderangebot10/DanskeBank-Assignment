using DanskeBank.Domain.CompanyAggregate;
using DanskeBank.Domain.OwnerAggregate;
using System;
using System.Collections.Generic;
using Xunit;

namespace DanskeBank.Domain.Test.Companies
{
    public class CompanyTest
    {
        [Trait("Category", "UnitTest")]
        [Fact]
        public void Company_ctor()
        {
            var o = new Company("test", "Lithuania", "+37062028789");

            Assert.NotNull(o);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public void Phrase_ctor_Overload()
        {
            var o = new Company("test", "Lithuania", "+37062028789", new List<Owner>());

            Assert.NotNull(o);
        }
    }
}
