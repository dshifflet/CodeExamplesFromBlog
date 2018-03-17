using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.Tests
{
    public class BasicTests : IClassFixture<NorthwindDbFixture>
    {
        private readonly NorthwindSetup _setup;

        //this is to display some output about the counts
        private readonly ITestOutputHelper _output;

        public BasicTests(ITestOutputHelper output, NorthwindDbFixture fixture)
        {
            _setup = fixture.Setup;
            _output = output;
        }

        [Fact]
        public void CanGetProductsViaLinq()
        {
            using (var db = _setup.GetDbNorthwind())
            {
                var query = db.Product.Where(o => o.Id > 25).OrderByDescending(o => o.ProductName)
                    .ToList();
                _output.WriteLine("Products: {0}", query.Count);
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
