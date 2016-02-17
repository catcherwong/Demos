using Dapper;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using NancyDemoForFormsauthentication.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NancyDemoForFormsauthentication.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["index"];
            };
            Get["/login"] = _ =>
            {
                return View["login"];
            };
            Post["/login"] = _ =>
            {
                var loginUser = this.Bind<SystemUser>();
                SystemUser user = GetValidUser(loginUser.SystemUserName, loginUser.SystemUserPassword);
                if (user == null)
                {
                    return Response.AsText("出错了", "text/html;charset=UTF-8");
                }
                return this.LoginAndRedirect(user.SystemUserId, fallbackRedirectUrl: "/secure");
            };
        }

        private readonly string sqlconnection =
               "Data Source=127.0.0.1;Initial Catalog=NancyDemo;User Id=sa;Password=dream_time1314;";

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            connection.Open();
            return connection;
        }

        private SystemUser GetValidUser(string name, string pwd)
        {
            using (IDbConnection conn = OpenConnection())
            {
                const string query = "select * from SystemUser where SystemUserName=@SystemUserName and SystemUserPassword=@SystemUserPassword";
                return conn.Query<SystemUser>(query, new { SystemUserName = name, SystemUserPassword = pwd }).SingleOrDefault();
            }
        }
    }
}