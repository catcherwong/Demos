namespace WebApi.Middlewares
{
    public class ApiAuthorizedOptions
    {
        public string Name { get; set; }

        public string EncryptKey { get; set; }
        
        public int ExpiredSecond { get; set; }
    }
}
