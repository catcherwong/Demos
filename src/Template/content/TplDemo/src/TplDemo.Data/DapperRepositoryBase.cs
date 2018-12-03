namespace TplDemo.Data
{
    using Microsoft.Extensions.Configuration;
    using System.Data.Common;
#if (MsSQL)
    using System.Data.SqlClient;
#elif (MySQL)
    using MySql.Data.MySqlClient;
#elif (PgSQL)
    using Npgsql;
#else 
    using Microsoft.Data.Sqlite;
#endif


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
#if (MsSQL)            
            return new SqlConnection(_connStr);
#elif (MySQL)            
            return new MySqlConnection(_connStr);
#elif (PgSQL)             
            return new NpgsqlConnection(_connStr);
#else              
            return new SqliteConnection(_connStr);
#endif              
        }     
    }
}
