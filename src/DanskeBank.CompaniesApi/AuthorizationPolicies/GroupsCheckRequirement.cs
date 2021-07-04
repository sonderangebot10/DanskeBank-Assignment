using Microsoft.AspNetCore.Authorization;

namespace DanskeBank.CompaniesApi.AuthorizationPolicies
{
    public class GroupsCheckRequirement : IAuthorizationRequirement
    {
        public string groups;

        public GroupsCheckRequirement(string groups)
        {
            this.groups = groups;
        }
    }
}
