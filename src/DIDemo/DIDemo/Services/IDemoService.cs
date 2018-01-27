namespace DIDemo.Services
{
    public interface IDemoService
    {
        string Get();
    }

    public class DemoServiceA : IDemoService
    {
        public string Get()
        {
            return "Service A";
        }
    }

    public class DemoServiceB : IDemoService
    {
        public string Get()
        {
            return "Service B";
        }
    }
}
