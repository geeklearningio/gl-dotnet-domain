namespace GeekLearning.Domain
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime RemoveTime(this DateTime datetime) => datetime.ToDate().DateTime;
        public static Date ToDate(this DateTime datetime) => new Date(datetime.Year, datetime.Month, datetime.Day);
    }
}
