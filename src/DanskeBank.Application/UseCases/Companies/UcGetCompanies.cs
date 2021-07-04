using DanskeBank.Application.Repositories;
using DanskeBank.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanskeBank.Application.UseCases.Companies
{
    public class UcGetCompanies : IUcGetCompanies
    {
        private readonly ICompaniesReadOnlyRepository _companiesReadOnlyRepository;

        public UcGetCompanies(ICompaniesReadOnlyRepository companiesReadOnlyRepository)
        {
            _companiesReadOnlyRepository = companiesReadOnlyRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _companiesReadOnlyRepository.GetCompaniesAsync();
        }

        /// <inheritdoc />
        public async Task<Company> GetCompanyDetailsAsync(Guid companyId)
        {
            return await _companiesReadOnlyRepository.GetCompanyDetailsAsync(companyId);
        }

        /// <inheritdoc />
        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            return await _companiesReadOnlyRepository.CompanyExistsAsync(companyId);
        }
    }
}
