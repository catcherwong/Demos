using Dapper;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using NancyDemoForFormsauthentication.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NancyDemoForFormsauthentication
{
    public class UserMapper : IUserMapper
    {
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "select * from SystemUser where SystemUserId=@SystemUserId";
                var user = conn.Query<SystemUser>(query, new { SystemUserId = identifier }).SingleOrDefault();

                if (user == null)
                {
                    return null;
                }
                else
                {
                    return new UserIdentity
                    {
                        UserName = user.SystemUserName,
                        Claims = new[] { "SystemUser"}
                    };
                }
            }
        }

        private readonly string sqlconnection =
                "Data Source=127.0.0.1;Initial Catalog=NancyDemo;User Id=sa;Password=dream_time1314;";

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            connection.Open();
            return connection;
        }
    }
}