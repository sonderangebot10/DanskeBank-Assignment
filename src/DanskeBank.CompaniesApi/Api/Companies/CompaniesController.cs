using Microsoft.AspNetCore.Mvc;
using DanskeBank.Application.Api.Models;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DanskeBank.Application.UseCases.Companies;
using System.Collections.Generic;
using DanskeBank.Domain.Companies;
using DanskeBank.CompaniesApi.Api.Json;
using System.Net;
using DanskeBank.Application.Resources;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DanskeBank.CompaniesApi.Api.Companies
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CompaniesController : Controller
    {
        private readonly IUcGetCompanies _getCompaniesUseCase;
        private readonly IUcModifyCompanies _modifyCompaniesUseCase;

        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(IUcGetCompanies getCompaniesUseCase, IUcModifyCompanies modifyCompaniesUseCase, ILogger<CompaniesController> logger)
        {
            _getCompaniesUseCase = getCompaniesUseCase;
            _modifyCompaniesUseCase = modifyCompaniesUseCase;
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
            var result = await _getCompaniesUseCase.GetCompaniesAsync();

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return await Task.FromResult(new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            });
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
            var result = await _getCompaniesUseCase.GetCompanyDetailsAsync(companyId);

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return await Task.FromResult(new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            });
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
            if (string.IsNullOrEmpty(company.Name) ||
                string.IsNullOrEmpty(company.Country) ||
                string.IsNullOrEmpty(company.PhoneNumber))
            {
                return BadRequest(Http_1_1.GetErr(
                        HttpStatusCode.BadRequest,
                        Resources.ERR_Request_Body_Cannot_Be_Empty(),
                        Resources.ERR_Argument_Exception(nameof(company), "not empty")));
            }

            var result = await _modifyCompaniesUseCase.CreateCompanyAsync(
                new Company(company.Name, company.Country, company.PhoneNumber));

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return await Task.FromResult(new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            });
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
            if (string.IsNullOrEmpty(companyId) || 
                string.IsNullOrEmpty(company.Name) ||
                string.IsNullOrEmpty(company.Country) ||
                string.IsNullOrEmpty(company.PhoneNumber))
            {
                return BadRequest(Http_1_1.GetErr(
                        HttpStatusCode.BadRequest,
                        Resources.ERR_Request_Body_Cannot_Be_Empty(),
                        Resources.ERR_Argument_Exception(nameof(company), "not empty")));
            }

            if (!await _getCompaniesUseCase.CompanyExistsAsync(companyId))
            {
                return BadRequest(Http_1_1.GetErr(
                    HttpStatusCode.BadRequest,
                    Resources.ERR_Incomplete_Request_Model(),
                    Resources.ERR_Argument_Company_Exception(nameof(companyId))));
            }

            var result = await _modifyCompaniesUseCase.UpdateCompanyAsync(
                new Company(companyId, company.Name, company.Country, company.PhoneNumber));

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return await Task.FromResult(new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            });
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
            if (string.IsNullOrEmpty(companyId) ||
                string.IsNullOrEmpty(owner.Name) ||
                string.IsNullOrEmpty(owner.SSN))
            {
                return BadRequest(Http_1_1.GetErr(
                        HttpStatusCode.BadRequest,
                        Resources.ERR_Request_Body_Cannot_Be_Empty(),
                        Resources.ERR_Argument_Exception(nameof(owner), "not empty")));
            }

            if(!await _getCompaniesUseCase.CompanyExistsAsync(companyId))
            {
                return BadRequest(Http_1_1.GetErr(
                    HttpStatusCode.BadRequest,
                    Resources.ERR_Incomplete_Request_Model(),
                    Resources.ERR_Argument_Company_Exception(nameof(companyId))));
            }

            var result = await _modifyCompaniesUseCase.AddOwnerAsync(companyId,
                new Owner(owner.Name, owner.SSN));

            var viewModelBody = result != null ? result.ToStdBody() : null;

            return await Task.FromResult(new ObjectResult(viewModelBody)
            {
                StatusCode = viewModelBody != null
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent
            });
        }
    }
}
