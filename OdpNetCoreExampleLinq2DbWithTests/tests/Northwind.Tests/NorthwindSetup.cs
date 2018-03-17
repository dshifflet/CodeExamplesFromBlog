using System;
using System.IO;
using System.Linq;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace Northwind.Tests
{
    public class NorthwindSetup
    {
        private readonly string[] _nameSpaces;
        private bool _namesChanged;
        private readonly string[] _buildCommands;
        
        public NorthwindSetup(string buildFile, params string[] nameSpaces)
        {
            if (buildFile == null) throw new ArgumentNullException(nameof(buildFile));
            if (!File.Exists(buildFile)) throw new FileNotFoundException(
                string.Format("Unable to find '{0}'", buildFile));
            
            _nameSpaces = nameSpaces ?? throw new ArgumentNullException(nameof(nameSpaces));

            using (var sr = new StreamReader(buildFile))
            {
                var s = sr.ReadToEnd();
                _buildCommands = s.Split(';');
            }
        }
        
        public DbNorthwind GetDbNorthwind()
        {
            var db = new DbNorthwind();
            
            //Have to change our table names because Linq2Db for Sqlite doesn't use the schema name
            ChangeTableNames(db, _nameSpaces);

            //build up the database from our script
            foreach (var cmd in _buildCommands)
            {
                db.Execute(cmd);
            }
            return db;
        }

        private void ChangeTableNames(DataConnection db, string[] nameSpaces)
        {
            //What this does?
            //Given a namespace look for classes we have registered with Linq2Db
            //and change the table name to schema_tablename.
            
            //Only do it once
            if (_namesChanged) return;
            _namesChanged = true;

            //get the classes in the namespaces
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && nameSpaces.Contains(t.Namespace)))
            {
                var descriptor = db.MappingSchema.GetEntityDescriptor(item);
                if (descriptor.SchemaName != null)
                {
                    //The reflection is here to deal with the generic methods.
                    var builder = db.MappingSchema.GetFluentMappingBuilder();
                    var method = typeof(FluentMappingBuilder).GetMethod("Entity");
                    var genericMethod = method.MakeGenericMethod(item);
                    var entityBuilder = genericMethod.Invoke(builder, new object[] { null });
                    var tblMethod = entityBuilder.GetType().GetMethod("HasTableName");                   
                    tblMethod.Invoke(entityBuilder, new object[] {
                        string.Format("{0}_{1}", descriptor.SchemaName, descriptor.TableName)});
                }
            }
        }
    }
}
