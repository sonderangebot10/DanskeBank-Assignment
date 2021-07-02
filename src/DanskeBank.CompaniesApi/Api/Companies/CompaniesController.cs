using Microsoft.AspNetCore.Mvc;
using DanskeBank.Application.Api.Models;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DanskeBank.CompaniesApi.Api.Companies
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CompaniesController : Controller
    {
        public CompaniesController()
        {

        }

        /// <summary>
        /// TEST
        /// </summary>
        /// <remarks>TEST</remarks>
        /// <returns>200Ok</returns>
        [HttpGet("TEST")]
        public async Task<IActionResult> Get()
        {
            var result = "SUCCESS";

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
