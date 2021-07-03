using DanskeBank.Domain.Companies;
using System.Threading.Tasks;

namespace DanskeBank.Application.UseCases.Companies
{
    public interface IUcModifyCompanies
    {
        /// <summary>
        /// creates a company async
        /// </summary>
        Task<Company> CreateCompanyAsync(Company company);
        /// <summary>
        /// updates a company async
        /// </summary>
        Task<Company> UpdateCompanyAsync(Company company);
        /// <summary>
        /// adds an owner to a company async
        /// </summary>
        Task<Company> AddOwnerAsync(string companyId, Owner owner);
    }
}
