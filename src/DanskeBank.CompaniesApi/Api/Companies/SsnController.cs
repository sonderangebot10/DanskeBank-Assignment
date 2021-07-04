using Microsoft.AspNetCore.Mvc;
using DanskeBank.Application.Api.Models;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DanskeBank.Application.Services;
using DanskeBank.CompaniesApi.Api.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace DanskeBank.CompaniesApi.Api.Companies
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class SsnController : Controller
    {
        private readonly ISsnService _ssnService;
        private readonly ILogger<SsnController> _logger;

        public SsnController(ISsnService ssnService, ILogger<SsnController> logger)
        {
            _ssnService = ssnService;
            _logger = logger;
        }

        /// <summary>
        /// Checks if SSN is correct
        /// </summary>
        /// <remarks>Correct format is '123-45-6789'</remarks>
        [HttpGet("{ssn}/Validate")]
        [Authorize(Policy = "SsnService")]
        [ProducesResponseType(typeof(TopLevelDocument<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TopLevelError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCompanies([FromRoute] string ssn)
        {
            var result = await _ssnService.CheckSSNAsync(ssn);

            var viewModelBody = 
                new SsnValidationResponseModel { Valid = result }.ToStdBody();

            return new ObjectResult(viewModelBody)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
