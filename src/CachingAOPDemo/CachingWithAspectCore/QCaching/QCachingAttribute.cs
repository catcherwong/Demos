namespace CachingWithAspectCore.QCaching
{
    using System;

    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class QCachingAttribute : Attribute
    {
        public int AbsoluteExpiration { get; set; } = 30;

        //add other settings ...
    }
}
