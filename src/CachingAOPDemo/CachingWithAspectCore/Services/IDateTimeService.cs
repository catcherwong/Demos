namespace CachingWithAspectCore.Services
{
    public interface IDateTimeService : QCaching.IQCaching
    {     
        [QCaching.QCaching(AbsoluteExpiration = 10)]
        string GetCurrentUtcTime();
    }

    public class DateTimeService : IDateTimeService
    {
        //[QCaching.QCaching(AbsoluteExpiration = 10)]
        public string GetCurrentUtcTime()
        {
            return System.DateTime.UtcNow.ToString();
        }
    }
}
