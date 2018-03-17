using System.Linq;
using Xunit;

namespace Northwind.Tests
{
    public class BasicTests : IClassFixture<NorthwindDbFixture>
    {
        private readonly NorthwindSetup _setup;

        public BasicTests(NorthwindDbFixture fixture)
        {
            _setup = fixture.Setup;
        }

        [Fact]
        public void CanGetProductsViaLinq()
        {
            using (var db = _setup.GetDbNorthwind())
            {
                var query = db.Product.Where(o => o.Id > 25).OrderByDescending(o => o.ProductName)
                    .ToList();
                Assert.True(query.Any());
            }
        }

        [Fact]
        public void CanGetCheapProductsViaLinq()
        {
            using (var db = _setup.GetDbNorthwind())
            {
                var query = db.Product.Where(o => o.UnitPrice < 20).OrderByDescending(o => o.ProductName)
                    .ToList();
                Assert.True(query.Any());
            }
        }
    }
}
