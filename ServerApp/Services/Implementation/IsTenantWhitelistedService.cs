using Microsoft.IdentityModel.Tokens;
using ServerApp.Models;

namespace ServerApp.Services.Implementation
{
    public class IsTenantWhitelistedService : Service
    {

        public IsTenantWhitelistedService(AppDbContext context) : base(context){}

        private bool IfTenantExist(string tenantId)
        {
            try
            {
                _context.Tenants.First(t => t.TenantId.Equals(tenantId));
                return true;
            }catch (Exception) { throw new Exception("Tenant with id " + tenantId + " doesn't exist."); }
            
        }
        private void PreConditions(string tenantId)
        {
            if (tenantId.Length == 0) throw new Exception("Tenant id is empty");
        }

        private bool Method(string tenantId)
        {
            return IfTenantExist(tenantId);
        }

        public bool Action(string tenantId)
        {
            PreConditions(tenantId);
            return Method(tenantId);
        }
    }
}
