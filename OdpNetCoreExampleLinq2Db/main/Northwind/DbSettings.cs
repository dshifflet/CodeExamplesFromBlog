using System.Collections.Generic;
using LinqToDB.Configuration;

namespace Northwind
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class NorthWindDbSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
        {
            get { yield break; }
        }

        public string DefaultConfiguration => "Oracle.Managed";
        public string DefaultDataProvider => "Oracle.Managed";

        public string ConnectionString { get; set; }

        public NorthWindDbSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "Northwind",
                        ProviderName = "Oracle.Managed",
                        ConnectionString = ConnectionString
                    };
            }
        }
    }
}
