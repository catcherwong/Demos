using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Middlewares;

namespace WebApi.Common
{
    public class DapperHelper
    {
        /// <summary>
        /// the dapper option
        /// </summary>
        private DapperOptions _options;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        public DapperHelper(IOptions<DapperOptions> options)
        {
            this._options = options.Value;
        }

        /// <summary>
        /// get the connection
        /// </summary>
        /// <returns></returns>
        private SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(_options.ConnectionString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// exec using T-SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool Execute(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(sql, param, transaction, commandTimeout, commandType);
                return count > 0;
            }
        }

        /// <summary>
        /// exec using T-SQL Asynchronous
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int count = await conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
                return count > 0;
            }
        }

        /// <summary>
        /// exec using stored procedure
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool ExecuteForProc(string storedProcedureName, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int count = conn.Execute(storedProcedureName, param, transaction, commandTimeout, commandType);
                return count > 0;
            }
        }

        /// <summary>
        /// exec using stored procedure Asynchronous
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteForProcAsync(string storedProcedureName, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int count = await conn.ExecuteAsync(storedProcedureName, param, transaction, commandTimeout, commandType);
                return count > 0;
            }
        }

        /// <summary>
        /// query using T-SQL 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IList<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }

        /// <summary>
        /// query using T-SQL Asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var res = await conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
                return res.ToList();
            }
        }

        /// <summary>
        /// query using stored procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IList<T> QueryForProc<T>(string storedProcedureName, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Query<T>(storedProcedureName, param, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }

        /// <summary>
        /// query using stored procedure Asynchronous
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<IList<T>> QueryForProcAsync<T>(string storedProcedureName, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var res = await conn.QueryAsync<T>(storedProcedureName, param, transaction, commandTimeout, commandType);
                return res.ToList();
            }
        }

        /// <summary>
        /// dynamic query using sql 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic Query(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Query(sql, param, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }

        /// <summary>
        /// dynamic query using sql Asynchronous
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<dynamic> QueryAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var res = await conn.QueryAsync(sql, param, transaction, commandTimeout, commandType);
                return res.ToList();
            }
        }

        /// <summary>
        /// dynamic query using stored procedure
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public dynamic QueryForProc(string storedProcedureName, object param = null, IDbTransaction transaction = null,
           bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Query(storedProcedureName, param, transaction, buffered, commandTimeout, commandType).ToList();
            }
        }

        /// <summary>
        /// dynamic query using stored procedure Asynchronous
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<dynamic> QueryForProcAsync(string storedProcedureName, object param = null, IDbTransaction transaction = null,
           int? commandTimeout = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection conn = OpenConnection())
            {
                var res = await conn.QueryAsync(storedProcedureName, param, transaction, commandTimeout, commandType);
                return res.ToList();
            }
        }
    }
}