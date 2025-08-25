
namespace Persistence.Infrastructure;

/// <summary>
/// Represents a connection string.
/// </summary>
public sealed class ConnectionString
{
    /// <summary>
    /// The connection strings key.
    /// </summary>
    public const string SettingsKey = "server=127.0.0.1; port=3306; database=stockapp_db; user=stockapp_dev; password=7Ja5q0JLM6me-+*61;Convert Zero Datetime=True";

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionString"/> class.
    /// </summary>
    /// <param name="value">The connection string value.</param>
    public ConnectionString(string value) => Value = value;

    /// <summary>
    /// Gets the connection string value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}

