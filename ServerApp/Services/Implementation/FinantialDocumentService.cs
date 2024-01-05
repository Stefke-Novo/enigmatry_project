using Microsoft.IdentityModel.Tokens;
using ServerApp.Models;
using ServerApp.Models.Queries;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata;

namespace ServerApp.Services.Implementation
{
    public class FinantialDocumentService : Service
    {
        public FinantialDocumentService(AppDbContext context) : base(context)
        {    
        }

        public DocumentData GetDocumentAndClientData(string tenantId, string documentId)
        {
            Preconditions(tenantId, documentId);
            
            var document = _context.Documents.Where(d => d.TenantId.Equals(tenantId) && d.DocumentId.Equals(documentId)).First();
            document.DocumentClient = _context.Clients.First(c => c.ClientId.Equals(document.ClientId));
            document.Transations = _context.Transactions.Where(t => t.ClientId.Equals(document.ClientId)&&t.TenantId.Equals(document.TenantId)&&t.DocumentId.Equals(document.DocumentId)).ToList<Transaction>();
            document.DocumentTenant = _context.Tenants.First(t => t.TenantId.Equals(document.TenantId));
            if (document == null)
                throw new Exception("No document found.");

            return new DocumentData
            {
                account_number = document.DocumentTenant.AccountNumber,
                balance = document.DocumentClient.Balance,
                currency = document.Currency,
                transactions = document.Transations,
            };
        }

        private void Preconditions(string tenantId, string documentId)
        {
            IsEmpty(tenantId, "Tenant id is empty.");
            IsEmpty(documentId, "Document id is empty.");
        }

        private void IsEmpty(string id,string message)
        {
            if (id.Length == 0) throw new Exception(message);
        }
    }
}
