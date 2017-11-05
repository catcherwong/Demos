namespace CachingWithCastle.Services
{
    public interface IDateTimeService
    {        
        string GetCurrentUtcTime();
    }

    public class DateTimeService : IDateTimeService, QCaching.IQCaching
    {
        [QCaching.QCaching(AbsoluteExpiration = 10)]
        public string GetCurrentUtcTime()
        {
            return System.DateTime.UtcNow.ToString();
        }
    }
}
