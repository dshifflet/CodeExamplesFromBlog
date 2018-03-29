using System.Linq;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
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
            using (var session = _fixture.OpenSession())
            using (var txn = session.BeginTransaction())
            {
                CreateTestProducts(session);
                var products = session.Query<Product>().Where(o => o.Id > 25).OrderByDescending(o => o.ProductName)
                    .ToList();
                _output.WriteLine("Products: {0}", products.Count);
                Assert.True(products.Any());
                txn.Commit();
            }
        }

        [Fact]
        public void CanGetCheapProductsViaLinq()
        {
            using (var session = _fixture.OpenSession())
            using (var txn = session.BeginTransaction())
            {
                CreateTestProducts(session);
                var products = session.Query<Product>().Where(o => o.UnitPrice < 20).OrderByDescending(o => o.ProductName)
                    .ToList();
                Assert.True(products.Any());
                txn.Commit();
            }
        }

        private void CreateTestProducts(ISession session)
        {
            //This creates the DBOs in the Database and inserts some test data.
            //This is an alternative to using SQL scripts
            //We have to recreate the schema because it's in memory and drops it when the connection is closed.
            new SchemaExport(NhFactory.Configuration).Execute(true, true, false, session.Connection, null);
            
            session.Save(new Product
            {
                Id = 1,
                CategoryId = 18,
                Discontinued = "N",
                ProductName = "Chai",
                QuantityPerUnit = "10 boxes x 20 bags",
                ReorderLevel = 5,
                SupplierId = 1,
                UnitPrice = 5.00,
                UnitsInStock = 39,
                UnitsOnOrder = 5
            });

            session.Save(new Product()
            {
                Id = 27,
                CategoryId = 31,
                Discontinued = "N",
                ProductName = "DogFood",
                QuantityPerUnit = "10 boxes x 20 bags",
                ReorderLevel = 5,
                SupplierId = 1,
                UnitPrice = 5.00,
                UnitsInStock = 39,
                UnitsOnOrder = 5
            });
        }
    }
}
