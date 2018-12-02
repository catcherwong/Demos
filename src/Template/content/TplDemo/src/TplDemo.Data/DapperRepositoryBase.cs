namespace TplDemo.Data
{
    using Microsoft.Extensions.Configuration;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class DapperRepositoryBase
    {
        protected readonly string _connStr;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public DapperRepositoryBase(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        /// <summary>
        /// Gets the db connection.
        /// </summary>
        /// <returns>The db connection.</returns>
        protected DbConnection GetDbConnection()
        {
            return new SqlConnection(_connStr);
        }

        /// <summary>
        /// Gets the db connection.
        /// </summary>
        /// <returns>The db connection.</returns>
        /// <param name="connStr">Conn string.</param>
        protected DbConnection GetDbConnection(string connStr)
        {
            return new SqlConnection(connStr);
        }
    }
}
