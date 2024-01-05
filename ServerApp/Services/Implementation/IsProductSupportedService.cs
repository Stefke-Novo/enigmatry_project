using Microsoft.IdentityModel.Tokens;
using ServerApp.Models;
using System.Linq;

namespace ServerApp.Services
{
    public class IsProductSupportedService : Service
    {

        public IsProductSupportedService(AppDbContext context) : base(context) { }

        private bool Method(string productCode)
        {
            return IfProductExists(productCode);
        }

        private void PreConditions(string productCode)
        {
            if (productCode.Length == 0) throw new Exception("Product code is empty");
        }
        public bool Action(string productCode)
        {
            PreConditions(productCode);
            return Method(productCode);

        }
        private bool IfProductExists(string productCode)
        {
            try
            {
                _context.Products.First(p => p.ProductCode.Equals(productCode));
            }
            catch (Exception){ throw new Exception("Product code is not supported."); }
                
            return true;
        }
    }
}
