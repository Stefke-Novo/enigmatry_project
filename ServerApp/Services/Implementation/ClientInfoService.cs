using Microsoft.EntityFrameworkCore;
using ServerApp.Models;
using ServerApp.Models.Queries;

namespace ServerApp.Services.Implementation
{
    public class ClientInfoService : Service
    {

        public ClientInfoService(AppDbContext context) : base(context){}
        public Document GetClient(string tenantId, string documentId)
        {
            Preconditions(tenantId,documentId);
            return Method(tenantId, documentId);
        }
        private Document Method(string tenantId, string documentId)
        {
            try
            {
                Document document = _context.Documents.Where(d => d.DocumentId.Equals(documentId) && d.TenantId.Equals(tenantId)).First();
                document.DocumentClient = _context.Clients.Where(c => c.ClientId == document.ClientId).First();
                return document;
            }
            catch (Exception) { throw new Exception("Document not found."); }
        }
        private void Preconditions(string _tenantId, string _documentId)
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
