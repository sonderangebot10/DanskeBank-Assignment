using DanskeBank.Application.Repositories;
using DanskeBank.Domain.Companies;
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

        public async Task<Company> AddOwnerAsync(string companyId, Owner owner)
        {
            return await _companiesWriteOnlyRepository.AddOwnerAsync(companyId, owner);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            return await _companiesWriteOnlyRepository.CreateCompanyAsync(company);
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            return await _companiesWriteOnlyRepository.UpdateCompanyAsync(company);
        }
    }
}
