using System;
using System.Globalization;


namespace Infrastructure.Common;

/// <summary>
/// Represents the machine date time service.
/// </summary>
internal sealed class MachineDateTime //: IDateTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;

    public  string GetCurrentTimestamp()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }
   
}

