using NHibernate;
using NHibernate.Driver;

namespace Northwind.Tests
{
    /// <summary>
    /// NHibernate driver for the Community Microsoft.Data.Sqlite data provider.
    /// </summary>
    public class MicrosoftDataSqliteDriver : ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Microsoft.Data.Sqlite"/>.
        /// </summary>
        /// <exception cref="HibernateException">
        /// Thrown when the <c>Microsoft.Data.Sqlite</c> assembly can not be loaded.
        /// </exception>
        public MicrosoftDataSqliteDriver()
            : base(
                "Microsoft.Data.Sqlite",
                "Microsoft.Data.Sqlite.SqliteConnection",
                "Microsoft.Data.Sqlite.SqliteCommand")
        {
        }

        public override bool UseNamedPrefixInSql => true;

        public override bool UseNamedPrefixInParameter => true;

        public override string NamedPrefix => "@";

        public override bool SupportsMultipleOpenReaders => false;
    }
}