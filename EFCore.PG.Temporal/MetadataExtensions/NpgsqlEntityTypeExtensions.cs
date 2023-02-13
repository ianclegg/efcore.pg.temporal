using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;

using  Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Metadata;
using  Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Metadata.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

/// <summary>
/// Extension methods for <see cref="IEntityType" /> for Npgsql-specific metadata.
/// </summary>
public static class NpgsqlEntityTypeExtensions
{
    /// <summary>
    ///     Returns a value indicating whether the entity type is mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns><see langword="true" /> if the entity type is mapped to a temporal table.</returns>
    public static bool IsTemporal(this IReadOnlyEntityType entityType)
        => entityType[NpgsqlAnnotationNames.IsTemporal] as bool? ?? false;

    /// <summary>
    ///     Sets a value indicating whether the entity type is mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="temporal">The value to set.</param>
    public static void SetIsTemporal(this IMutableEntityType entityType, bool temporal)
        => entityType.SetOrRemoveAnnotation(NpgsqlAnnotationNames.IsTemporal, temporal);

    /// <summary>
    ///     Sets a value indicating whether the entity type is mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="temporal">The value to set.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The configured value.</returns>
    public static bool? SetIsTemporal(
        this IConventionEntityType entityType,
        bool? temporal,
        bool fromDataAnnotation = false)
        => (bool?)entityType.SetOrRemoveAnnotation(
            NpgsqlAnnotationNames.IsTemporal,
            temporal,
            fromDataAnnotation)?.Value;

    /// <summary>
    ///     Gets the configuration source for the temporal table setting.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The configuration source for the temporal table setting.</returns>
    public static ConfigurationSource? GetIsTemporalConfigurationSource(this IConventionEntityType entityType)
        => entityType.FindAnnotation(NpgsqlAnnotationNames.IsTemporal)?.GetConfigurationSource();

    /// <summary>
    ///     Returns a value representing the name of the period start property of the entity mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>Name of the period start property.</returns>
    public static string? GetPeriodStartPropertyName(this IReadOnlyEntityType entityType)
        => (entityType is RuntimeEntityType)
            ? throw new InvalidOperationException(CoreStrings.RuntimeModelMissingData)
            : entityType[NpgsqlAnnotationNames.TemporalPeriodStartPropertyName] as string;

    /// <summary>
    ///     Sets a value representing the name of the period start property of the entity mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodStartPropertyName">The value to set.</param>
    public static void SetPeriodStartPropertyName(this IMutableEntityType entityType, string? periodStartPropertyName)
        => entityType.SetAnnotation(NpgsqlAnnotationNames.TemporalPeriodStartPropertyName, periodStartPropertyName);

    /// <summary>
    ///     Sets a value representing the name of the period start property of the entity mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodStartPropertyName">The value to set.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The configured value.</returns>
    public static string? SetPeriodStartPropertyName(
        this IConventionEntityType entityType,
        string? periodStartPropertyName,
        bool fromDataAnnotation = false)
        => (string?)entityType.SetAnnotation(
            NpgsqlAnnotationNames.TemporalPeriodStartPropertyName,
            periodStartPropertyName,
            fromDataAnnotation)?.Value;

    /// <summary>
    ///     Gets the configuration source for the temporal table period start property name setting.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The configuration source for the temporal table period start property name setting.</returns>
    public static ConfigurationSource? GetPeriodStartPropertyNameConfigurationSource(this IConventionEntityType entityType)
        => entityType.FindAnnotation(NpgsqlAnnotationNames.TemporalPeriodStartPropertyName)?.GetConfigurationSource();

    /// <summary>
    ///     Returns a value representing the name of the period end property of the entity mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>Name of the period start property.</returns>
    public static string? GetPeriodEndPropertyName(this IReadOnlyEntityType entityType)
        => (entityType is RuntimeEntityType)
            ? throw new InvalidOperationException(CoreStrings.RuntimeModelMissingData)
            : entityType[NpgsqlAnnotationNames.TemporalPeriodEndPropertyName] as string;

    /// <summary>
    ///     Sets a value representing the name of the period end property of the entity mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodEndPropertyName">The value to set.</param>
    public static void SetPeriodEndPropertyName(this IMutableEntityType entityType, string? periodEndPropertyName)
        => entityType.SetAnnotation(NpgsqlAnnotationNames.TemporalPeriodEndPropertyName, periodEndPropertyName);

    /// <summary>
    ///     Sets a value representing the name of the period end property of the entity mapped to a temporal table.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <param name="periodEndPropertyName">The value to set.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>The configured value.</returns>
    public static string? SetPeriodEndPropertyName(
        this IConventionEntityType entityType,
        string? periodEndPropertyName,
        bool fromDataAnnotation = false)
        => (string?)entityType.SetAnnotation(
            NpgsqlAnnotationNames.TemporalPeriodEndPropertyName,
            periodEndPropertyName,
            fromDataAnnotation)?.Value;

    /// <summary>
    ///     Gets the configuration source for the temporal table period end property name setting.
    /// </summary>
    /// <param name="entityType">The entity type.</param>
    /// <returns>The configuration source for the temporal table period end property name setting.</returns>
    public static ConfigurationSource? GetPeriodEndPropertyNameConfigurationSource(this IConventionEntityType entityType)
        => entityType.FindAnnotation(NpgsqlAnnotationNames.TemporalPeriodEndPropertyName)?.GetConfigurationSource();
}
