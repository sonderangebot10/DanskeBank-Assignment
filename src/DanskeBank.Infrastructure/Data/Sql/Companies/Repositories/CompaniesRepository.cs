using DanskeBank.Domain.CompanyAggregate;
using DanskeBank.Domain.OwnerAggregate;
using DanskeBank.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanskeBank.Infrastructure.Data.Sql.Companies.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly Context _context;

        public CompaniesRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public async Task<Company> AddOwnerAsync(Guid companyId, Owner owner)
        {
            var newCompany =
                await _context.Companies.FirstOrDefaultAsync(company => company.Id.Equals(companyId));

            var newOwner =
                new Owner
                {
                    Name = owner.Name,
                    SSN = owner.SSN
                };

            if(newOwner.IsTransient())
            {
                newCompany.AddOwner(newOwner);
            }

            return await GetCompanyDetailsAsync(companyId);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            var companies =
                await _context.Companies.Where(company => company.Id.Equals(companyId)).ToListAsync();

            var result = companies.Count > 0 ? true : false;

            return result;
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            var newCompany =
                new Company
                {
                    Name = company.Name,
                    Country = company.Country,
                    PhoneNumber = company.PhoneNumber
                };

            await _context.Companies.AddAsync(newCompany);

            return await GetCompanyDetailsAsync(newCompany.Id);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            var result = await _context.Companies.ToListAsync();

            return result;
        }

        public async Task<Company> GetCompanyDetailsAsync(Guid companyId)
        {
            var result = 
                await _context.Companies.FirstOrDefaultAsync(company => company.Id.Equals(companyId));
            
            return result;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            var entityCompany =
                await _context.Companies.FirstOrDefaultAsync(c => c.Id.Equals(company.Id));

            entityCompany.Name = company.Name;
            entityCompany.PhoneNumber = company.PhoneNumber;
            entityCompany.Country = company.Country;

            return await GetCompanyDetailsAsync(company.Id);
        }
    }
}