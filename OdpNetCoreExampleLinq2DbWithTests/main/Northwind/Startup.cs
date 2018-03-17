using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Northwind.Models;

namespace Northwind
{
    public static class DbNorthwindStartup
    {
        private static bool _started;
        public static void Init(ILinqToDBSettings settings)
        {
            if (!_started)
            {
                DataConnection.DefaultSettings = settings;
                _started = true;
            }
        }
    }

    public class DbNorthwind : DataConnection
    {
        public DbNorthwind() : base("Northwind")
        {

        }

        //Register our mappings
        public ITable<Product> Product => GetTable<Product>();

        // ... other tables ...
    }
}
