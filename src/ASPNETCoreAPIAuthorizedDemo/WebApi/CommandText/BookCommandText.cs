namespace WebApi.CommandText
{
    public class BookCommandText
    {
        public static string GetBooks = @"select Id,Title,Author from Books";

        public static string GetBookById = @"USP_GETBYID_BOOK";
    }
}
