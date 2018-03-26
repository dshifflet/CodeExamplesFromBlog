using System.Linq;
using Northwind.Models;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.Tests
{
    public class BasicTests : IClassFixture<NorthwindDbFixture>
    {
        //this is to display some output about the counts
        private readonly ITestOutputHelper _output;
        private readonly NorthwindDbFixture _fixture;

        public BasicTests(ITestOutputHelper output, NorthwindDbFixture fixture)
        {
            _output = output;
            _fixture = fixture;
        }

        [Fact]
        public void CanGetProductsViaLinq()
        {
            using(var session = _fixture.OpenSession())
            {
                var query = session.Query<Product>().Where(o => o.Id > 25).OrderByDescending(o => o.ProductName)
                    .ToList();
                _output.WriteLine("Products: {0}", query.Count);
                Assert.True(query.Any());
            }
        }

        [Fact]
        public void CanGetCheapProductsViaLinq()
        {
            using (var session = _fixture.OpenSession())
            {
                var query = session.Query<Product>().Where(o => o.UnitPrice < 20).OrderByDescending(o => o.ProductName)
                    .ToList();
                Assert.True(query.Any());
            }
        }
    }
}
