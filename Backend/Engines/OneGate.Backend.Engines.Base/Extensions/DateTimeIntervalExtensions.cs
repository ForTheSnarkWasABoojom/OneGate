using System;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Engines.Base.Extensions
{
    public static class DateTimeIntervalExtensions
    {
        public static DateTime AddInterval(this DateTime dateTime, OhlcIntervalDto interval)
        {
            return interval switch
            {
                OhlcIntervalDto.m1 => dateTime.AddMinutes(1),
                OhlcIntervalDto.m5 => dateTime.AddMinutes(5),
                OhlcIntervalDto.m15 => dateTime.AddMinutes(15),
                OhlcIntervalDto.m30 => dateTime.AddMinutes(30),
                OhlcIntervalDto.H1 => dateTime.AddHours(1),
                OhlcIntervalDto.H4 => dateTime.AddHours(4),
                OhlcIntervalDto.D1 => dateTime.AddDays(1),
                OhlcIntervalDto.M1 => dateTime.AddMonths(1),
                _ => dateTime
            };
        }
        
        public static DateTime NormalizeByInterval(this DateTime dateTime, OhlcIntervalDto interval)
        {
            return interval switch
            {
                OhlcIntervalDto.m1 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute, 0, 0),
                OhlcIntervalDto.m5 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute / 5 * 5, 0, 0),
                OhlcIntervalDto.m15 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute / 15 * 15, 0, 0),
                OhlcIntervalDto.m30 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                    dateTime.Minute / 30 * 30, 0, 0),
                OhlcIntervalDto.H1 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, 0),
                OhlcIntervalDto.H4 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour / 4 * 4, 0, 0,
                    0),
                OhlcIntervalDto.D1 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0),
                OhlcIntervalDto.M1 => new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0),
                _ => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0)
            };
        }
    }
}