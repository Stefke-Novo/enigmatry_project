using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using ServerApp;
using ServerApp.Models;
using ServerApp.Models.Queries;
using ServerApp.Services.Implementation;

namespace UnitTesting.ServiceTests
{
    [TestFixture]
    internal class ClientServiceTest
    {
        private readonly Mock<AppDbContext> _appDbContext;
        private readonly ClientInfoService _clientService;
        private List<Client> GetFakeClients()
        {
            return new List<Client>
            {
                new Client(){ClientId="Client1",ClientVAT="10"},
                new Client(){ClientId="Client2",ClientVAT="20"},
                new Client(){ClientId="Client3",ClientVAT="30"}
            };
        }
        private List<Document> GetFakeDocuments()
        {
            return new List<Document>()
            {
                new Document(){ClientId="CLient1",DocumentId="24e739933bd54531b9b0d44495c60a88",TenantId="9b0d5ce433554c99bd9aaf34c8b0e129",DocumentClient=new Client(){ClientId="Client1",ClientVAT="10"},DocumentTenant=new Tenant(){TenantId="9b0d5ce433554c99bd9aaf34c8b0e129"}},
                new Document(){ClientId="Client2",DocumentId="7acbf7892b2f4a1296b22c3b36631f1a",TenantId="f42a122464c349969b4348fadd25fd6e",DocumentClient=new Client(){ClientId="Client2",ClientVAT="20"}, DocumentTenant= new Tenant(){TenantId="f42a122464c349969b4348fadd25fd6e"}},
                new Document(){ClientId="Client3",DocumentId="53b71d0367f448f1bfd73b70ca32d1d3",TenantId="8aa372f6c7aa40128eb56f68a3298879",DocumentClient=new Client(){ClientId="Client3",ClientVAT="30"}, DocumentTenant= new Tenant(){TenantId="8aa372f6c7aa40128eb56f68a3298879"}}
            };
        }
        private List<Tenant> GetFakeTenants()
        {
            return new List<Tenant> 
            { 
                new Tenant(){TenantId="9b0d5ce433554c99bd9aaf34c8b0e129"},
                new Tenant(){TenantId="f42a122464c349969b4348fadd25fd6e"},
                new Tenant(){TenantId="8aa372f6c7aa40128eb56f68a3298879"}
            };
        }
        public ClientServiceTest()
        {
            _appDbContext = new Mock<AppDbContext>();
            _appDbContext.Setup(c => c.Clients).ReturnsDbSet(this.GetFakeClients());
            _appDbContext.Setup(d => d.Documents).ReturnsDbSet(this.GetFakeDocuments());
            _appDbContext.Setup(t => t.Tenants).ReturnsDbSet(this.GetFakeTenants());
            this._clientService=new ClientInfoService(this._appDbContext.Object);
            
        }
        [Test]
        public void ActionTest()
        {
            Document client1 = _clientService.GetClient("9b0d5ce433554c99bd9aaf34c8b0e129", "24e739933bd54531b9b0d44495c60a88");
            Client client2 = new Client() { ClientId = "Client1", ClientVAT = "10" };
            //when item exists in database
            Assert.IsTrue(client1.ClientId.Equals(client2.ClientId)&&client1.DocumentClient.ClientVAT.Equals(client2.ClientVAT));
            //empty 1. argument
            Assert.Throws<Exception>(() => _clientService.GetClient("", ";lsakjda;slkjd;"));
            //empty 2. argument
            Assert.Throws<Exception>(() => _clientService.GetClient("sakjdhaksjhd", ""));
            //when item doesn't exist in database
            Assert.Throws<Exception>(() => _clientService.GetClient("9b0d5ce433554c99bd9aaf34c8b0e129", "slakjhdalskjhdlskjahdlajshalskdj"));
        }
    }
}
