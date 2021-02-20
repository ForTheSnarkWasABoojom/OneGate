namespace OneGate.Backend.Engines.Shared.Extensions
{
    public static class DateTimeIntervalExtensions
    {
        /*public static DateTime AddInterval(this DateTime dateTime, IntervalDto interval)
        {
            return interval switch
            {
                IntervalDto.m1 => dateTime.AddMinutes(1),
                IntervalDto.m5 => dateTime.AddMinutes(5),
                IntervalDto.m15 => dateTime.AddMinutes(15),
                IntervalDto.m30 => dateTime.AddMinutes(30),
                IntervalDto.H1 => dateTime.AddHours(1),
                IntervalDto.H4 => dateTime.AddHours(4),
                IntervalDto.D1 => dateTime.AddDays(1),
                IntervalDto.M1 => dateTime.AddMonths(1),
                _ => dateTime
            };
        }
        
        public static DateTime NormalizeByInterval(this DateTime dateTime, IntervalDto interval)
        {
            return interval switch
            {
                IntervalDto.m1 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute, 0, 0),
                IntervalDto.m5 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute / 5 * 5, 0, 0),
                IntervalDto.m15 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute / 15 * 15, 0, 0),
                IntervalDto.m30 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute / 30 * 30, 0, 0),
                IntervalDto.H1 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, 0),
                IntervalDto.H4 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour / 4 * 4, 0, 0,
                    0),
                IntervalDto.D1 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0),
                IntervalDto.M1 => new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0),
                _ => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0)
            };
        }*/
    }
}