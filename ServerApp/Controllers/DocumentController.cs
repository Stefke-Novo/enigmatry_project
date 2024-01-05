using Azure;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Models.Queries;
using ServerApp.Models.Request;
using ServerApp.Services;
using ServerApp.Services.Implementation;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class DocumentController : Controller
    {
        private readonly IsProductSupportedService _isProductSupportedService;
        private readonly IsTenantWhitelistedService _isTenantWhitelistedService;
        private readonly ClientInfoService _clientInfoService;
        private readonly FinantialDocumentService _finantialDocumentService;
        private readonly IfClientExists _ifClientExists;
        private readonly AddClientDataService _addClientDataService;
        public DocumentController(
            IsProductSupportedService isProductSupportedService,
            IsTenantWhitelistedService isTenantWhitelistedService,
            ClientInfoService clientInfoService,
            FinantialDocumentService finantialDocumentService,
            IfClientExists ifClientExists,
            AddClientDataService addClientDataService
            ) 
        {
            _clientInfoService = clientInfoService;
            _isProductSupportedService = isProductSupportedService;
            _isTenantWhitelistedService = isTenantWhitelistedService;
            _finantialDocumentService = finantialDocumentService; 
            _ifClientExists = ifClientExists;
            _addClientDataService = addClientDataService;
        }
        [HttpPost]
        public ObjectResult getDocument(DocumentRequest req)
        {
            try
            {
                if (
                    !(_isProductSupportedService.Action(req.ProductCode) &&
                      _isTenantWhitelistedService.Action(req.TenantId) && 
                      _ifClientExists.Action(req.DocumentId, req.TenantId))
                   )
                    throw new Exception("Something went wrong");

                Document document = _clientInfoService.GetClient(req.TenantId, req.DocumentId);
                var clientInfo = _addClientDataService.AddClientInformation(document.DocumentClient.ClientVAT);
                var financialDocument = _finantialDocumentService.GetDocumentAndClientData(req.TenantId, req.DocumentId);
                var transactions = financialDocument.transactions.Select(t => new
                {
                    t.TransactionId,
                    t.Amount,
                    date = ""+t.Date.Day+"/"+t.Date.Month+"/"+t.Date.Year,
                    t.Description
                }).ToList();

                return Ok(new { data = new 
                { 
                    account_number = financialDocument.account_number,
                    balance = financialDocument.balance,
                    currency = financialDocument.currency,
                    transactions = transactions.ToList()
                },company = clientInfo});
            }catch(Exception e) 
            {
                ObjectResult br = new(e.Message)
                {
                    StatusCode = 403
                };
                return br; 
            }
        }
    }
}
