namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public static class NpgsqlTemporalAnnotationNames
{
    /// <summary>
    /// </summary>
    public const string Prefix = "NpgsqlTemporal:";

    /// <summary>
    /// </summary>
    public const string IsTemporal = Prefix + "IsTemporal";

    /// <summary>
    /// 
    /// </summary>
    public const string TemporalAsOfPointInTime = Prefix + "TemporalAsOfPointInTime";

    /// <summary>
    /// </summary>
    public const string TemporalPeriodStartPropertyName = Prefix + "TemporalPeriodStartPropertyName";

    /// <summary>
    /// </summary>
    public const string TemporalPeriodStartColumnName = Prefix + "TemporalPeriodStartColumnName";

    /// <summary>
    /// </summary>
    public const string TemporalPeriodEndPropertyName = Prefix + "TemporalPeriodEndPropertyName";

    /// <summary>
    /// </summary>
    public const string TemporalPeriodEndColumnName = Prefix + "TemporalPeriodEndColumnName";
}
