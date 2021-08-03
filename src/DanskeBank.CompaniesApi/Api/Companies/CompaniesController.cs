using Microsoft.AspNetCore.Mvc;
using DanskeBank.Application.Api.Models;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using DanskeBank.CompaniesApi.Api.Json;
using System.Net;
using DanskeBank.Application.Resources;
using Microsoft.Extensions.Logging;
using System;
using DanskeBank.Domain.CompanyAggregate;
using DanskeBank.Domain.OwnerAggregate;

namespace DanskeBank.CompaniesApi.Api.Companies
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesRepository _companiesRepository;

        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(ICompaniesRepository companiesRepository, ILogger<CompaniesController> logger)
        {
            _companiesRepository = companiesRepository;
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of all companies
        /// </summary>
        /// <remarks></remarks>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(TopLevelDocument<List<Company>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TopLevelDocument<List<Company>>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(TopLevelError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCompanies()
        {
            var result = await _companiesRepository.GetCompaniesAsync();

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            };
        }

        /// <summary>
        /// Returns a specific comapny
        /// </summary>
        /// <remarks></remarks>
        /// <param name="companyId">Id of the company</param>
        [HttpGet("{companyId}")]
        [ProducesResponseType(typeof(TopLevelDocument<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TopLevelDocument<Company>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(TopLevelError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompany([FromRoute] string companyId)
        {
            if (!Guid.TryParse(companyId, out var cId))
            {
                return BadRequest(Http_1_1.GetErr(
                    HttpStatusCode.BadRequest,
                    Resources.ERR_Incomplete_Request_Model(),
                    Resources.ERR_Argument_Company_Exception(companyId)));
            }

            var result = await _companiesRepository.GetCompanyDetailsAsync(cId);

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            };
        }

        /// <summary>
        /// Creates a company
        /// </summary>
        /// <remarks></remarks>
        /// <param name="company">the company object</param>
        [HttpPost]
        [ProducesResponseType(typeof(TopLevelDocument<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TopLevelError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyModel company)
        {
            var result = await _companiesRepository.CreateCompanyAsync(
                new Company(company.Name, company.Country, company.PhoneNumber));

            await _companiesRepository.UnitOfWork.SaveChangesAsync();

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            };
        }

        /// <summary>
        /// Updates a company
        /// </summary>
        /// <remarks></remarks>
        /// <param name="company">the company object</param>
        /// <param name="companyId">the company id</param>
        [HttpPatch("{companyId}")]
        [ProducesResponseType(typeof(TopLevelDocument<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TopLevelError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompany([FromRoute] string companyId, [FromBody] UpdateCompanyModel company)
        {
            if (!Guid.TryParse(companyId, out var cId) ||
                !await _companiesRepository.CompanyExistsAsync(cId))
            {
                return BadRequest(Http_1_1.GetErr(
                    HttpStatusCode.BadRequest,
                    Resources.ERR_Incomplete_Request_Model(),
                    Resources.ERR_Argument_Company_Exception(companyId)));
            }

            var result = await _companiesRepository.UpdateCompanyAsync(
                new Company(company.Name, company.Country, company.PhoneNumber));

            await _companiesRepository.UnitOfWork.SaveChangesAsync();

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            };
        }

        /// <summary>
        /// Adds and owner to a company
        /// </summary>
        /// <remarks></remarks>
        /// <param name="owner">the owner object</param>
        /// <param name="companyId">the company id</param>
        [HttpPost("{companyId}/AddOwner")]
        [ProducesResponseType(typeof(TopLevelDocument<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TopLevelError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOwner([FromRoute] string companyId, [FromBody] OwnerModel owner)
        {
            if (!Guid.TryParse(companyId, out var cId) ||
                !await _companiesRepository.CompanyExistsAsync(cId))
            {
                return BadRequest(Http_1_1.GetErr(
                    HttpStatusCode.BadRequest,
                    Resources.ERR_Incomplete_Request_Model(),
                    Resources.ERR_Argument_Company_Exception(companyId)));
            }

            var result = await _companiesRepository.AddOwnerAsync(cId,
                new Owner(owner.Name, owner.SSN));

            await _companiesRepository.UnitOfWork.SaveChangesAsync();

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            };
        }
    }
}
