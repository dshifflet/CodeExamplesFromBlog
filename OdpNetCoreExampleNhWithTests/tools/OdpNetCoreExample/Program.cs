using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NHibernate.Dialect;
using NHibernate.Driver;
using Northwind;
using Northwind.Models;

namespace OdpNetCoreExample
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json");

            Configuration = builder.Build();

            GetData();
        }

        static void GetData()
        {
            using (var sessionFactory =
                NhFactory.CreateNhSessionFactory<Oracle10gDialect, OracleManagedDataClientDriver>(Configuration.GetConnectionString("NorthwindDb")))
            {
                using (var session = sessionFactory.OpenSession())
                using (var txn = session.BeginTransaction())
                {
                    var query = session.Query<Product>().Where(o => o.Id > 25).OrderByDescending(o => o.ProductName)
                        .ToList();

                    foreach (var item in query.ToList())
                    {
                        Console.WriteLine(item.ProductName);
                    }
                    txn.Commit();
                }
            }
        }
    }
}
