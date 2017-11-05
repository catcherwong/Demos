namespace CachingWithCastle.BLL
{
    public class DateTimeBLL : QCaching.IQCaching
    {
        [QCaching.QCaching(AbsoluteExpiration = 10)]
        public virtual string GetCurrentUtcTime()
        {
            return System.DateTime.UtcNow.ToString();
        }
    }
}
