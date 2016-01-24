using Dapper;
using MovieDemoWithOwin.Models;
using MovieDemoWithOwin.ViewModels;
using Nancy;
using Nancy.ModelBinding;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MovieDemoWithOwin.Modules
{
    public class MovieModule : NancyModule
    {
        public MovieModule() : base("/movie")
        {
            Get["/"] = _ => ShowMovieIndexPage();

            Get["/edit/{movieId}"] = _ => ShowMovieEditPage(_.movieId);
            Post["/edit/{movieId}"] = _ =>
            {
                var movie = this.Bind<Movie>();
                return MovieEdit(movie);
            };

            Get["/create"] = _ => ShowMovieCreatePage();
            Post["/create"] = _ =>
            {
                var movie = this.Bind<Movie>();
                return MovieCreate(movie);
            };

            Get["/delete/{movieId}"] = _ => MovieDelete(_.movieId);
        }

        private readonly string _sqlconnection =
                "Data Source=192.168.0.71;Initial Catalog=NancyDemo;User Id=sa;Password=dream_time1314;";

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_sqlconnection);
            connection.Open();
            return connection;
        }

        private dynamic MovieDelete(int movieId)
        {
            string deleteMovieStoredProcedure = @"up_DeleteMovieByMovieId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@MovieId", movieId);
            using (IDbConnection conn = OpenConnection())
            {
                conn.Execute(deleteMovieStoredProcedure, dynamicParameters, null, null, CommandType.StoredProcedure);
                return Response.AsRedirect("/movie");
            }
        }

        private dynamic MovieCreate(Movie movie)
        {
            string createMovieStoredProcedure = @"up_InsertMovie";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@MovieName", movie.MovieName);
            dynamicParameters.Add("@MovieTypeId", movie.MovieTypeId);
            dynamicParameters.Add("@MovieAddTime", movie.MovieAddTime);

            using (IDbConnection conn = OpenConnection())
            {
                conn.Execute(createMovieStoredProcedure, dynamicParameters, null, null, CommandType.StoredProcedure);
                return Response.AsRedirect("/movie");
            }
        }

        private dynamic ShowMovieCreatePage()
        {
            string getALLTypeStoredProcedure = @"up_GetAllMovieTypes";
            using (IDbConnection conn = OpenConnection())
            {
                MovieTypeListViewModel viewModel = new MovieTypeListViewModel
                {
                    MovieTypes = conn.Query<MovieType>(getALLTypeStoredProcedure, null, null, true, null, CommandType.StoredProcedure)
                };
                return View["Create", viewModel];
            }
        }

        private dynamic MovieEdit(Movie movie)
        {
            string updateMovieStoredProcedure = @"up_UpdateMovieByMovieId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@MovieId", movie.MovieId);
            dynamicParameters.Add("@MovieName", movie.MovieName);
            dynamicParameters.Add("@MovieTypeId", movie.MovieTypeId);

            using (IDbConnection conn = OpenConnection())
            {
                conn.Execute(updateMovieStoredProcedure, dynamicParameters, null, null, CommandType.StoredProcedure);
                return Response.AsRedirect("/movie");
            }
        }

        private dynamic ShowMovieEditPage(int movieId)
        {
            string getOneMovieStoredProcedure = @"up_GetMovieByMovieId";
            string getALLTypeStoredProcedure = @"up_GetAllMovieTypes";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@MovieId", movieId);

            using (IDbConnection conn = OpenConnection())
            {
                var movieViewModel = conn.Query<MovieViewModel>(getOneMovieStoredProcedure, dynamicParameters, null, true, null, CommandType.StoredProcedure).SingleOrDefault();
                ViewBag.typeList = conn.Query<MovieType>(getALLTypeStoredProcedure, null, null, true, null, CommandType.StoredProcedure);
                return View["Edit", movieViewModel];
                
            }
        }

        private dynamic ShowMovieIndexPage()
        {
            using (IDbConnection conn = OpenConnection())
            {
                string getAllMoviesStoredProcedure = @"up_GetAllMovies";
                MovieListViewModel viewModel = new MovieListViewModel
                {
                    Movies = conn.Query<MovieViewModel>(getAllMoviesStoredProcedure,
                    null, null, true, null, CommandType.StoredProcedure)
                };
                return View["Index", viewModel];
            }
        }
    }
}