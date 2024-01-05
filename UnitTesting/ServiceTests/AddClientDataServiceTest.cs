using Moq;
using Moq.EntityFrameworkCore;
using ServerApp;
using ServerApp.Models;
using ServerApp.Services.Implementation;

namespace UnitTesting.ServiceTests
{
    internal class AddClientDataServiceTest
    {
        private readonly Mock<AppDbContext> _appDbContext;
        private readonly AddClientDataService _service;

        private List<Client> GetFakeClients()
        {
            return new List<Client>
            {
                new Client(){ ClientId="Client1",ClientVAT="10",CompanyType="small"},
                new Client(){ClientId="Client2", ClientVAT="20",CompanyType="medium"},
                new Client(){ClientId="Client3",ClientVAT="30",CompanyType="large"}
            };
        }
        public AddClientDataServiceTest() 
        {
            _appDbContext = new Mock<AppDbContext>();
            _appDbContext.Setup(c => c.Clients).ReturnsDbSet(this.GetFakeClients());
            _service = new AddClientDataService(_appDbContext.Object);
        }
        [Test]
        public void AddingDataBasedOnCompanyVAT()
        {
            //When item exists in database and it's small company
            Assert.Throws<Exception>(()=>_service.AddClientInformation("10"));
            //When item exists and it's medium or large
            Assert.IsInstanceOf<IQueryable>(_service.AddClientInformation("20"));
            //when field is empty
            Assert.Throws<Exception>(()=>_service.AddClientInformation(""));
            //If item doesn't exist in database
            Assert.Throws<Exception>(()=>_service.AddClientInformation("50"));
        }
    }
}
