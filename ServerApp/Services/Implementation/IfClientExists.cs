using ServerApp.Models;

namespace ServerApp.Services.Implementation
{
    public class IfClientExists : Service
    {
        public IfClientExists(AppDbContext context) : base(context)
        {
        }

        public bool Action(string documentId, string tenantId)
        {
            PreConditions(documentId, tenantId);
            try
            {
                Document document = _context.Documents
                 .First(d => d.DocumentId.Equals(documentId) && d.TenantId.Equals(tenantId));
                document.DocumentClient=_context.Clients.First(c=>c.ClientId.Equals(document.ClientId));
                return true;
            }
            catch (Exception) { return false; }
        }
        private void PreConditions(string _tenantId, string _documentId)
        {
            IfEmpty(_tenantId, "Tenant id is empty");
            IfEmpty(_documentId, "Document id is empty");
        }
        private static void IfEmpty(string Id, string exceptionMessage)
        {
            if (Id.Length == 0) throw new Exception(exceptionMessage);
        }
    }
}
