using CloudHub.Domain.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudHub.API.Filters
{
    public class TenantFilter : IAuthorizationFilter
    {
        public TenantFilter(ITenantsService tenantsService) => _tenantsService = tenantsService;

        private readonly ITenantsService _tenantsService;


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string host = context.HttpContext.Request.Host.Value;
            string tenantId = host.Split('.')[0];
            tenantId = context.HttpContext.Request.Host.Port?.ToString() ?? "80";
            _tenantsService.SetTenant(tenantId);
        }
    }
}
