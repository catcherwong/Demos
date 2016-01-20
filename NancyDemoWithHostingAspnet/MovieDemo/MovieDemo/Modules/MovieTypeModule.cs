using Dapper;
using MovieDemo.Models;
using MovieDemo.ViewModels;
using Nancy;
using Nancy.ModelBinding;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MovieDemo.Modules
{
    public class MovieTypeModule : NancyModule
    {
        public MovieTypeModule() : base("/movie-type")
        {
            Get["/"] = parameters => ShowMovieTypeIndexPage();

            Get["/edit/{typeId}"] = _ => ShowMovieTypeEditPage(_.typeId);
            Post["/edit/{typeId}"] = parameters =>
            {
                var type = this.Bind<MovieType>();
                return MovieTypeEdit(type);
            };

            Get["/create"] = _ => ShowMovieTypeCreatePage();
            Post["/create"] = parameters =>
            {
                var type = this.Bind<MovieType>();
                return MovieTypeCreate(type);
            };

            Get["/delete/{typeId}"] = _ => DeleteMovieType(_.typeId);
        }

        private readonly string _sqlconnection =
                 "Data Source=127.0.0.1;Initial Catalog=NancyDemo;User Id=sa;Password=123;";

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_sqlconnection);
            connection.Open();
            return connection;
        }

        private dynamic DeleteMovieType(int typeId)
        {
            string deleteMovieTypeStoredProcedure = @"up_DeleteMovieTypeByTypeId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@TypeId", typeId);
            using (IDbConnection conn = OpenConnection())
            {
                conn.Execute(deleteMovieTypeStoredProcedure, dynamicParameters, null, null, CommandType.StoredProcedure);
                return Response.AsRedirect("/movie-type");
            }
        }

        private dynamic MovieTypeCreate(MovieType type)
        {
            string createMovieTypeStoredProcedure = @"up_InsertMovieType";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@TypeName", type.TypeName);
            using (IDbConnection conn = OpenConnection())
            {
                conn.Execute(createMovieTypeStoredProcedure, dynamicParameters, null, null, CommandType.StoredProcedure);
                return Response.AsRedirect("/movie-type");
            }
        }

        private dynamic ShowMovieTypeCreatePage()
        {
            return View["Create"];
        }

        private dynamic MovieTypeEdit(MovieType type)
        {
            string updateMovieTypeStoredProcedure = @"up_UpdateMovieTypeByTypeId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@TypeId", type.TypeId);
            dynamicParameters.Add("@TypeName", type.TypeName);

            using (IDbConnection conn = OpenConnection())
            {
                conn.Execute(updateMovieTypeStoredProcedure, dynamicParameters, null, null, CommandType.StoredProcedure);
                return Response.AsRedirect("/movie-type");
            }
        }

        private dynamic ShowMovieTypeEditPage(int typeId)
        {
            string getOneMovieTypeStoredProcedure = @"up_GetMovieTypeByTypeId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@TypeId", typeId);
            using (IDbConnection conn = OpenConnection())
            {
                var movieType = conn.Query<MovieType>(getOneMovieTypeStoredProcedure, dynamicParameters, null, true, null, CommandType.StoredProcedure).SingleOrDefault();
                return View["Edit", movieType];
            }
        }

        private dynamic ShowMovieTypeIndexPage()
        {
            using (IDbConnection conn = OpenConnection())
            {
                string getAllMovieTypeStoredProcedure = @"up_GetAllMovieTypes";
                MovieTypeListViewModel viewModel = new MovieTypeListViewModel
                {
                    MovieTypes = conn.Query<MovieType>(getAllMovieTypeStoredProcedure,
                    null, null, true, null, CommandType.StoredProcedure)
                };
                return View["Index", viewModel];
            }
        }
    }
}