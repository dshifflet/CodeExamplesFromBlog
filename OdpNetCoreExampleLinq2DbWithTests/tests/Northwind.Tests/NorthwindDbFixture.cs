using System;

namespace Northwind.Tests
{
    public class NorthwindDbFixture : IDisposable
    {
        public NorthwindSetup Setup { get; }

        public NorthwindDbFixture()
        {
            DbNorthwindStartup.Init(new SqliteDbSettings("Data Source=:memory:;"));
            Setup = new NorthwindSetup("build_northwind.sql", "Northwind.Models");
        }

        public void Dispose()
        {
        }
    }
}