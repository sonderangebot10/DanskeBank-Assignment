using DanskeBank.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanskeBank.Application.UseCases.Companies
{
    public interface IUcGetCompanies
    {
        /// <summary>
        /// gets companies async
        /// </summary>
        Task<IEnumerable<Company>> GetCompaniesAsync();
        /// <summary>
        /// gets details of a company async
        /// </summary>
        Task<Company> GetCompanyDetailsAsync(Guid companyId);
        /// <summary>
        /// checks whether a company exists by given company id
        /// </summary>
        Task<bool> CompanyExistsAsync(Guid companyId);
    }
}
