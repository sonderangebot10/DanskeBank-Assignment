
using DanskeBank.Domain.Companies;
using System.Threading.Tasks;

namespace DanskeBank.Application.Repositories
{
    public interface ICompaniesWriteOnlyRepository
    {
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> UpdateCompanyAsync(Company company);
        Task<Company> AddOwnerAsync(string companyId, Owner owner);
    }
}
