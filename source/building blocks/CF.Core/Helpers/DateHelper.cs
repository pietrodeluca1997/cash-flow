namespace CF.Core.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetBrazilianTime()
        {
            TimeZoneInfo brazilianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.Now, brazilianTimeZone);
        }
    }
}
