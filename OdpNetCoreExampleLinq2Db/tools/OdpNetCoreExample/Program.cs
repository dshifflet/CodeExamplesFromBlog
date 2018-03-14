using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Northwind;

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

            DbNorthwindStartup.Init(Configuration.GetConnectionString("NorthwindDb"));

            GetData();
        }

        static void GetData()
        {
            using (var db = new DbNorthwind())
            {
                var query = db.Product.Where(o => o.Id > 25).OrderByDescending(o=>o.ProductName)
                    .ToList();

                foreach (var item in query.ToList())
                {
                    Console.WriteLine(item.ProductName);
                }
            }
        }
    }
}
