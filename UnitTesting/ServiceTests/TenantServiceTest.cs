using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using ServerApp;
using ServerApp.Models;
using ServerApp.Services.Implementation;

namespace UnitTesting.ServiceTests
{
    [TestFixture]
    internal class TenantServiceTest
    {
        private readonly Mock<AppDbContext> appDbContext;
        private readonly IsTenantWhitelistedService tenantService;

        private List<Tenant> GetFakeTenants() 
        { 
            return new List<Tenant>() 
            { 
                new() { TenantId = "d84157e37bee476588c5e22a75e2d3fc" } ,
                new() { TenantId = "edf32e4360a0480c883ff6a8fbb9ada0" } ,
                new() { TenantId = "8146899f3e784db5a07494b963552b19" }
            }; 
        }

        public TenantServiceTest()
        {
            appDbContext = new Mock<AppDbContext>();
            appDbContext.Setup<DbSet<Tenant>>(t => t.Tenants).ReturnsDbSet(this.GetFakeTenants());
            tenantService = new IsTenantWhitelistedService(appDbContext.Object);
        }
        [Test]
        public void IsTenantWhiteListedTest()
        {
            Assert.Multiple(() => 
            {
                //when tenant exists
                Assert.That(tenantService.Action("d84157e37bee476588c5e22a75e2d3fc"), Is.True);
                //when false parameter is sent
                Assert.Throws<Exception>(()=> tenantService.Action(""));
                //when tenant doesn't exist
                Assert.Throws<Exception>(() => tenantService.Action("lsadslakjdhsalkjdhslkjdhlskjdhslkjd"));
            });
        }
    }
}
