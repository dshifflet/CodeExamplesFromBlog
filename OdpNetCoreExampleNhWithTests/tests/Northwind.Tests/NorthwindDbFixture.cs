using System;
using System.IO;
using NHibernate;
using NHibernate.Dialect;

namespace Northwind.Tests
{
    public class NorthwindDbFixture : IDisposable
    {
        public ISessionFactory SessionFactory { get; }
        private readonly string[] _buildCommands;

        public NorthwindDbFixture()
        {
            //DataSource=:memory: makes Sqlite use in memory     
            SessionFactory = NhFactory.CreateNhSessionFactory<SQLiteDialect, MicrosoftDataSqliteDriver> ("DataSource=:memory:");

            using (var sr = new StreamReader("build_northwind.sql"))
            {
                var s = sr.ReadToEnd();
                _buildCommands = s.Split(';');
            }
        }

        public ISession OpenSession()
        {
            var session = SessionFactory.OpenSession();
            foreach (var sql in _buildCommands)
            {
                using (var cmd = session.Connection.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            return session;
        }

        public void Dispose()
        {
        }
    }
}