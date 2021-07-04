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

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _companiesReadOnlyRepository.GetCompaniesAsync();
        }

        public async Task<Company> GetCompanyDetailsAsync(Guid companyId)
        {
            return await _companiesReadOnlyRepository.GetCompanyDetailsAsync(companyId);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            return await _companiesReadOnlyRepository.CompanyExistsAsync(companyId);
        }
    }
}
