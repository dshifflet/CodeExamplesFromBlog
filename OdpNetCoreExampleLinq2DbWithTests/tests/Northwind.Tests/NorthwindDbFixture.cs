using System;

namespace Northwind.Tests
{
    public class NorthwindDbFixture : IDisposable
    {
        public NorthwindSetup Setup { get; }

        public NorthwindDbFixture()
        {
            //DataSource=:memory: makes Sqlite use in memory
            DbNorthwindStartup.Init(new SqliteDbSettings("Data Source=:memory:;"));
            //Let's run this which will create our database and change our mapping names
            Setup = new NorthwindSetup("build_northwind.sql", "Northwind.Models");
        }

        public void Dispose()
        {
        }
    }
}