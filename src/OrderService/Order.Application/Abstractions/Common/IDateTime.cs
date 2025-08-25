using System;

namespace Shared.Application.Abstractions.Common
{
    /// <summary>
    /// Represents the interface for getting the current date and time.
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Gets the current date and time in UTC format.
        /// </summary>
        DateTime UtcNow { get; }
        string GetCurrentTimestamp();
        DateTime ExtractDateFromString(string? Datestring, string Timezone = "Africa/Nairobi");
        DateTime ConvertDateByTimezone(string? Timezone, DateTime dateTime);
        DateOnly GetDateOnly(int Year, int Month, int Day = 1);
        int LastDayOfMonth(int year, int month);
        Dictionary<int, string> GetCaledarMonths();
        Dictionary<string, string> GetCaledarMonthList();
    }
}