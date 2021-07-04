using DanskeBank.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanskeBank.Application.Repositories
{
    public interface ICompaniesReadOnlyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyDetailsAsync(Guid companyId);
        Task<bool> CompanyExistsAsync(Guid companyId);
    }
}
