using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DanskeBank.CompaniesApi.AuthorizationPolicies
{
    public class GroupsCheckHandler : AuthorizationHandler<GroupsCheckRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string AuthroizationHeader = "Authorization";

        public GroupsCheckHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
                                                  GroupsCheckRequirement requirement)
        {
            var headers = _httpContextAccessor.HttpContext.Request.Headers;
            if (headers.TryGetValue(AuthroizationHeader, out var token))
            {
                if(requirement.groups.Equals(token))
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}   
