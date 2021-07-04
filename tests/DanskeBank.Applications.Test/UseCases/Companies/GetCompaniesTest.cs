using Moq;
using Xunit;
using DanskeBank.Application.Repositories;
using System.Collections.Generic;
using DanskeBank.Domain.Companies;
using DanskeBank.Application.UseCases.Companies;

namespace DanskeBank.Applications.Test.UseCases.Companies
{
    public class GetCompaniesTest
    {
        [Trait("Category", "UnitTest")]
        [Fact]
        public async void GetCompanies_GetAllCompanies()
        {
            var mockCompaniesReadOnlyRepo = new Mock<ICompaniesReadOnlyRepository>();

            var companies = new List<Company>
            {
                new Company("Google", "United States of America", "+12165454844651", "Lithuania", new List<Owner> { new Owner("742-22-4554", "Jeff") })
            };

            mockCompaniesReadOnlyRepo.SetupSequence(e => e.GetCompaniesAsync())
                .ReturnsAsync(companies);

            var uc = new UcGetCompanies(mockCompaniesReadOnlyRepo.Object);

            var output = await uc.GetCompaniesAsync();

            Assert.Equal(companies, output);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void GetCompanies_GetSpecficiCompany()
        {
            var mockCompaniesReadOnlyRepo = new Mock<ICompaniesReadOnlyRepository>();

            var company = new Company("Google", "United States of America", "+12165454844651", "Lithuania", new List<Owner> { new Owner("742-22-4554", "Jeff") });

            mockCompaniesReadOnlyRepo.SetupSequence(e => e.GetCompanyDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(company);

            var uc = new UcGetCompanies(mockCompaniesReadOnlyRepo.Object);

            var output = await uc.GetCompanyDetailsAsync("test");

            Assert.Equal(company, output);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void GetCompanies_CompanyExists()
        {
            var mockCompaniesReadOnlyRepo = new Mock<ICompaniesReadOnlyRepository>();

            mockCompaniesReadOnlyRepo.SetupSequence(e => e.CompanyExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var uc = new UcGetCompanies(mockCompaniesReadOnlyRepo.Object);

            var output = await uc.CompanyExistsAsync("test");

            Assert.True(output);
        }
    }
}
