using DanskeBank.Application.Repositories;
using DanskeBank.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanskeBank.Infrastructure.Data.Sql.Companies.Repositories
{
    public class CompaniesRepository : ICompaniesReadOnlyRepository, ICompaniesWriteOnlyRepository
    {
        private readonly Context _context;

        public CompaniesRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Company> AddOwnerAsync(Guid companyId, Owner owner)
        {
            var entityCompany =
                await _context.Companies.FirstOrDefaultAsync(company => company.Id.Equals(companyId));

            var entityOwner =
                new Entities.Owner
                {
                    Name = owner.Name,
                    SSN = owner.SSN
                };

            entityCompany.Owners.Add(entityOwner);

            await _context.SaveChangesAsync();

            return await GetCompanyDetailsAsync(companyId);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            var entityCompanies =
                await _context.Companies.Where(company => company.Id.Equals(companyId)).ToListAsync();

            var result = entityCompanies.Count > 0 ? true : false;

            return result;
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            var entityCompany =
                new Entities.Company
                {
                    Name = company.Name,
                    Country = company.Country,
                    PhoneNumber = company.PhoneNumber,
                    Owners = new List<Entities.Owner>()
                };

            await _context.Companies.AddAsync(entityCompany);

            await _context.SaveChangesAsync();

            return await GetCompanyDetailsAsync(entityCompany.Id);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            var entityCompanies = await _context.Companies.ToListAsync();

            var result = new List<Company>();
            foreach(var entityCompany in entityCompanies)
            {
                var owners = new List<Owner>();
                foreach (var entityOwner in entityCompany.Owners)
                {
                    var owner = new Owner(entityOwner.Name, entityOwner.SSN);
                    owners.Add(owner);
                }

                var company = new Company(
                    entityCompany.Id,
                    entityCompany.Name,
                    entityCompany.Country,
                    entityCompany.PhoneNumber,
                    owners);

                result.Add(company);
            }

            return result;
        }

        public async Task<Company> GetCompanyDetailsAsync(Guid companyId)
        {
            var entityCompany = 
                await _context.Companies.FirstOrDefaultAsync(company => company.Id.Equals(companyId));

            if(entityCompany == null)
            {
                return null;
            }

            var owners = new List<Owner>();
            foreach (var entityOwner in entityCompany.Owners)
            {
                var owner = new Owner(entityOwner.Name, entityOwner.SSN);
                owners.Add(owner);
            }

            var result = new Company(entityCompany.Id, entityCompany.Name, entityCompany.Country, entityCompany.PhoneNumber, owners);
            return result;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            var entityCompany =
                await _context.Companies.FirstOrDefaultAsync(c => c.Id.Equals(company.Id));

            entityCompany.Name = company.Name;
            entityCompany.PhoneNumber = company.PhoneNumber;
            entityCompany.Country = company.Country;

            await _context.SaveChangesAsync();

            return await GetCompanyDetailsAsync(company.Id);
        }
    }
}