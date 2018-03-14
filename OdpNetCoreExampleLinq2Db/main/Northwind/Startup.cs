using LinqToDB;
using LinqToDB.Data;
using Northwind.Models;

namespace Northwind
{
    public static class DbNorthwindStartup
    {
        private static bool _started;
        public static void Init(string connectionString)
        {
            if (!_started)
            {
                DataConnection.DefaultSettings = new NorthWindDbSettings(connectionString);
                _started = true;
            }
        }
    }

    public class DbNorthwind : DataConnection
    {
        public DbNorthwind() : base("Northwind") { }

        //Register our mappings
        public ITable<Product> Product => GetTable<Product>();

        // ... other tables ...
    }
}
