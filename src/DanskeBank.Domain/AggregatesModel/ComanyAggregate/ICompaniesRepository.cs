using DanskeBank.Domain.OwnerAggregate;
using DanskeBank.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanskeBank.Domain.CompanyAggregate
{
    public interface ICompaniesRepository : IRepository<Company>
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyDetailsAsync(Guid companyId);
        Task<bool> CompanyExistsAsync(Guid companyId);
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> UpdateCompanyAsync(Company company);
        Task<Company> AddOwnerAsync(Guid companyId, Owner owner);

    }
}
