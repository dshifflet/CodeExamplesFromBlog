using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace OdpNetCoreExample
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json");
            Configuration = builder.Build();
            GetData(Configuration.GetConnectionString("NorthwindDb"));
        }

        static void GetData(string connectionString)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "select category_name from northwind.categories";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["category_name"]);
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}
