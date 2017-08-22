using Dapper;
using Microsoft.Data.Sqlite;
using Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using System.Data;
using System.IO;

namespace API
{
    public class StudentModule : NancyModule
    {
        private string _connStr;

        public StudentModule() : base("api/students")
        {
            this._connStr = $"Data Source ={Path.Combine(Directory.GetCurrentDirectory(), "rpdemo.db")}";

            Get("/", async (_, ct) =>
            {
                using (IDbConnection conn = new SqliteConnection(_connStr))
                {
                    conn.Open();

                    var sql = "select s.id,s.no,s.name,s.gender from students s";

                    var res = await conn.QueryAsync<Student>(sql);

                    return Negotiate
                            .WithMediaRangeModel(new MediaRange("application/json"), res)
                            .WithMediaRangeModel(new MediaRange("application/xml"), res);
                }
            });

            Get("/{id}", async (_, ct) =>
            {
                using (IDbConnection conn = new SqliteConnection(_connStr))
                {
                    conn.Open();

                    var sql = @"select s.id,s.no,s.name,s.gender 
                                from students s
                                where s.id = @id ";

                    var res = await conn.QueryFirstOrDefaultAsync<Student>(sql, new
                    {
                        id = (int)_.id
                    });

                    return Negotiate
                            .WithMediaRangeModel(new MediaRange("application/json"), res)
                            .WithMediaRangeModel(new MediaRange("application/xml"), res);                    
                }
            });

            Post("/", async (_, ct) =>
            {
                var student = this.Bind<Student>();

                using (IDbConnection conn = new SqliteConnection(_connStr))
                {
                    conn.Open();

                    var sql = @"insert into students(no,name,gender)
                                values (@no, @name, @gender)";

                    var res = await conn.ExecuteAsync(sql, new
                    {
                        no = student.No,
                        name = student.Name,
                        gender = student.Gender
                    });

                    if (res > 0)
                    {
                        return HttpStatusCode.Created;                        
                    }
                    else
                    {
                        return HttpStatusCode.InternalServerError;
                    }
                }
            });

            Put("/", async (_, ct) =>
            {
                var student = this.Bind<Student>();

                using (IDbConnection conn = new SqliteConnection(_connStr))
                {
                    conn.Open();

                    var sql = @"update students
                                set no = @no,name=@name,gender=@gender
                                where id = @id";

                    var res = await conn.ExecuteAsync(sql, new
                    {
                        id = student.Id,
                        no = student.No,
                        name = student.Name,
                        gender = student.Gender
                    });

                    if (res > 0)
                    {
                        return HttpStatusCode.OK;                        
                    }
                    else
                    {
                        return HttpStatusCode.InternalServerError;
                    }
                }
            });

            Delete("/{id}", async (_, ct) =>
            {
                using (IDbConnection conn = new SqliteConnection(_connStr))
                {
                    conn.Open();

                    var sql = @"delete students 
                                where id = @id";

                    var res = await conn.ExecuteAsync(sql, new
                    {
                        id = (int)_.Id
                    });

                    if (res > 0)
                    {
                        return HttpStatusCode.OK;
                    }
                    else
                    {
                        return HttpStatusCode.InternalServerError;
                    }
                }
            });
        }
    }
}
