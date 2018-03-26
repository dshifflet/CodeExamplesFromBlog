using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Northwind
{
    public static class NhFactory
    {
        public static HbmMapping[] HbmMappings { get; private set; }

        public static ISessionFactory CreateNhSessionFactory<TDialect, TDriver>(string connectionString, Assembly[] assemblies = null, bool showSql = true) 
            where TDialect : Dialect
            where TDriver : IDriver
        {
            if (assemblies == null)
            {
                assemblies = new[] { Assembly.GetExecutingAssembly() };
            }

            var nhConfiguration = new Configuration();
            nhConfiguration.Cache(properties => properties.Provider<HashtableCacheProvider>());

            nhConfiguration.DataBaseIntegration(dbi =>
            {
                dbi.Dialect<TDialect>();
                dbi.Driver<TDriver>();
                dbi.ConnectionProvider<DriverConnectionProvider>();
                dbi.IsolationLevel = IsolationLevel.ReadCommitted;
                dbi.ConnectionString = connectionString;
                dbi.Timeout = 60;
                dbi.LogFormattedSql = true;
                dbi.LogSqlInConsole = false;
            });
            if (showSql)
            {
                nhConfiguration.Properties["show_sql"] = "true";
            }


            if (HbmMappings == null || !HbmMappings.Any())
            {
                RegisterMappings(assemblies);
            }
            if (HbmMappings != null)
            {
                HbmMappings.ToList().ForEach(nhConfiguration.AddMapping);
                var assembly = Assembly.GetExecutingAssembly();
                nhConfiguration.AddAssembly(assembly);
                var sessionFactory = nhConfiguration.BuildSessionFactory();
                return sessionFactory;
            }
            throw new HibernateConfigException("Unable to find any mappings for NHibernate.");
        }

        public static void RegisterMappings(Assembly[] assemblies)
        {
            //Register things that are classes ending with Mapper and in the Mappings namespace
            var mapper = new ModelMapper();
            
            foreach (Type t in GetMappings(assemblies))
            {
                var method = typeof(ModelMapper).GetMethods().FirstOrDefault(
                        o => o.Name.Equals("AddMapping") &&
                        o.GetParameters().Length == 0 &&
                        o.ContainsGenericParameters
                );

                if (method == null)
                {
                    throw new InvalidOperationException("Could not retrieve AddMapping method");
                }

                method
                    .MakeGenericMethod(t).Invoke(mapper, null);
            }

            var mappings = new List<ModelMapper>
                           {
                               mapper
                           };
            var hibernateMappings = mappings.Select(map =>
            {
                var hbm = map.CompileMappingForAllExplicitlyAddedEntities();
                hbm.autoimport = false;
                return hbm;
            }).ToArray();
            HbmMappings = hibernateMappings;
        }

        public static IEnumerable<Type> GetMappings(IEnumerable<Assembly> assemblies)
        {
            var result = new List<Type>();
            foreach (var assembly in assemblies)
            {
                result.AddRange(assembly.GetTypes()
                    .Where(o => o.Name.EndsWith("Mapping") && o.Namespace != null &&
                                o.Namespace.EndsWith("Mappings"))
                    .ToArray());
            }
            return result;
        }
    }
}
