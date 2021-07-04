using DanskeBank.Application.Repositories;
using DanskeBank.Application.UseCases.Companies;
using DanskeBank.Domain.Companies;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DanskeBank.Applications.Test.UseCases.Companies
{
    public class ModifyCompaniesTest
    {
        [Trait("Category", "UnitTest")]
        [Fact]
        public async void ModifyCompanies_CreateCompany()
        {
            var mockCompaniesWriteOnlyRepo = new Mock<ICompaniesWriteOnlyRepository>();

            var company = new Company(Guid.NewGuid(), "United States of America", "+12165454844651", "Lithuania", new List<Owner> { new Owner("742-22-4554", "Jeff") });

            mockCompaniesWriteOnlyRepo.SetupSequence(e => e.CreateCompanyAsync(It.IsAny<Company>()))
                .ReturnsAsync(company);

            var uc = new UcModifyCompanies(mockCompaniesWriteOnlyRepo.Object);

            var output = await uc.CreateCompanyAsync(company);

            Assert.Equal(company, output);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void ModifyCompanies_UpdateCompany()
        {
            var mockCompaniesWriteOnlyRepo = new Mock<ICompaniesWriteOnlyRepository>();

            var company = new Company(Guid.NewGuid(), "United States of America", "+12165454844651", "Lithuania", new List<Owner> { new Owner("742-22-4554", "Jeff") });

            mockCompaniesWriteOnlyRepo.SetupSequence(e => e.UpdateCompanyAsync(It.IsAny<Company>()))
                .ReturnsAsync(company);

            var uc = new UcModifyCompanies(mockCompaniesWriteOnlyRepo.Object);

            var output = await uc.UpdateCompanyAsync(company);

            Assert.Equal(company, output);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void ModifyCompanies_AddOwner()
        {
            var mockCompaniesWriteOnlyRepo = new Mock<ICompaniesWriteOnlyRepository>();

            var company = new Company(Guid.NewGuid(), "United States of America", "+12165454844651", "Lithuania", new List<Owner> { new Owner("742-22-4554", "Jeff") });

            mockCompaniesWriteOnlyRepo.SetupSequence(e => e.AddOwnerAsync(It.IsAny<Guid>(), It.IsAny<Owner>()))
                .ReturnsAsync(company);

            var uc = new UcModifyCompanies(mockCompaniesWriteOnlyRepo.Object);

            var output = await uc.AddOwnerAsync(Guid.NewGuid(), new Owner());

            Assert.Equal(company, output);
        }
    }
}
