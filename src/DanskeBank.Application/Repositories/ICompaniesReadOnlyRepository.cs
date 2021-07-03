using DanskeBank.Domain.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanskeBank.Application.Repositories
{
    public interface ICompaniesReadOnlyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyDetailsAsync(string companyId);
        Task<bool> CompanyExistsAsync(string companyId);
    }
}
