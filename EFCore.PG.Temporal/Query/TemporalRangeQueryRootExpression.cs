namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query;

/// <summary>
/// </summary>
public abstract class TemporalRangeQueryRootExpression : TemporalQueryRootExpression
{
    /// <summary>
    /// </summary>
    protected TemporalRangeQueryRootExpression(
        IEntityType entityType,
        DateTime from,
        DateTime to)
        : base(entityType)
    {
        From = from;
        To = to;
    }

    /// <summary>
    /// </summary>
    protected TemporalRangeQueryRootExpression(
        IAsyncQueryProvider queryProvider,
        IEntityType entityType,
        DateTime from,
        DateTime to)
        : base(queryProvider, entityType)
    {
        From = from;
        To = to;
    }

    /// <summary>
    /// </summary>
    public virtual DateTime From { get; }

    /// <summary>
    /// </summary>
    public virtual DateTime To { get; }

    /// <summary>
    /// </summary>
    public override bool Equals(object? obj)
        => obj != null
            && (ReferenceEquals(this, obj)
                || obj is TemporalRangeQueryRootExpression queryRootExpression
                && Equals(queryRootExpression));

    private bool Equals(TemporalRangeQueryRootExpression queryRootExpression)
        => base.Equals(queryRootExpression)
            && Equals(From, queryRootExpression.From)
            && Equals(To, queryRootExpression.To);

    /// <summary>
    /// </summary>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(base.GetHashCode());
        hashCode.Add(From);
        hashCode.Add(To);

        return hashCode.ToHashCode();
    }
}
