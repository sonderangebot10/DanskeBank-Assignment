using DanskeBank.Application.Repositories;
using DanskeBank.Domain.Companies;
using System;
using System.Threading.Tasks;

namespace DanskeBank.Application.UseCases.Companies
{
    public class UcModifyCompanies : IUcModifyCompanies
    {
        private readonly ICompaniesWriteOnlyRepository _companiesWriteOnlyRepository;

        public UcModifyCompanies(ICompaniesWriteOnlyRepository companiesWriteOnlyRepository)
        {
            _companiesWriteOnlyRepository = companiesWriteOnlyRepository;
        }

        /// <inheritdoc />
        public async Task<Company> AddOwnerAsync(Guid companyId, Owner owner)
        {
            return await _companiesWriteOnlyRepository.AddOwnerAsync(companyId, owner);
        }

        /// <inheritdoc />
        public async Task<Company> CreateCompanyAsync(Company company)
        {
            return await _companiesWriteOnlyRepository.CreateCompanyAsync(company);
        }

        /// <inheritdoc />
        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            return await _companiesWriteOnlyRepository.UpdateCompanyAsync(company);
        }
    }
}
