using Microsoft.IdentityModel.Tokens;
using ServerApp.Models;
using ServerApp.Models.Queries;
using System.Collections.Generic;

namespace ServerApp.Services.Implementation
{
    public class AddClientDataService : Service
    {
        public AddClientDataService(AppDbContext context) : base(context) { }
        public ClientData AddClientInformation(string clientVAT)
        {
            Preconditions(clientVAT);
            return IfClientVATExists(clientVAT);
        }

        private void Preconditions(string clientVAT)
        {
            if (clientVAT.Length == 0) throw new Exception("Client VAT value is empty.");
        }
        private ClientData IfClientVATExists(string clientVAT)
        {
            var result = _context.Clients.Where(client => client.ClientVAT.Equals(clientVAT));

            IsResultNullOrEmpty(clientVAT, result);
            IsCompanyTypeSmall(result.First<Client>());

            return result.Select(client => new ClientData{ RegistrationNumber = client.RegistrationNumber, CompanyType = client.CompanyType }).First();
        }

        private void IsResultNullOrEmpty(string clientVAT, IQueryable<Client> result)
        {
            if (result.IsNullOrEmpty())
                throw new Exception("Client with client VAT " + clientVAT + " not found.");
        }

        public void IsCompanyTypeSmall(Client result)
        {
            if (result.CompanyType.Equals("small"))
                throw new Exception("Company type is small.");
        }
    }
}
