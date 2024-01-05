using ServerApp.Services.Implementation;
using Moq;
using ServerApp;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;
using Moq.EntityFrameworkCore;
using ServerApp.Services;

namespace UnitTesting.ServiceTests
{
    [TestFixture]
    internal class ProductServiceTest
    {
        private readonly Mock<AppDbContext> appDbContext;
        private readonly IsProductSupportedService productService;
        private List<Product> GetFakeProducts()
        {
            return new List<Product>()
            {
                new (){ProductCode = "ProductA"},
                new (){ProductCode = "ProductB"},
                new (){ProductCode = "ProductC"}
            };
        }
        public ProductServiceTest() 
        {
            appDbContext = new Mock<AppDbContext>();
            appDbContext.Setup<DbSet<Product>>(c => c.Products).ReturnsDbSet(this.GetFakeProducts());
            productService = new IsProductSupportedService(appDbContext.Object);
        }

        [Test]
        public void IsSupportedTest()
        {
            Assert.Multiple(() =>
            {
                //when product code is null
                Assert.Throws<Exception>(()=> productService.Action(""));
                //when value exist
                Assert.That(productService.Action("ProductA"), Is.True);
                //when vlaue doesn't exist
                Assert.Throws<Exception>(()=> productService.Action("ProductD"));
            });
        }
    }
}
