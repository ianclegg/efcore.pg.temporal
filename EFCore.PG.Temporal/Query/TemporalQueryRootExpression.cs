namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query;

/// <summary>
/// </summary>
public abstract class TemporalQueryRootExpression : EntityQueryRootExpression
{
    /// <summary>
    /// </summary>
    protected TemporalQueryRootExpression(IEntityType entityType)
        : base(entityType)
    {
    }

    /// <summary>
    /// </summary>
    protected TemporalQueryRootExpression(IAsyncQueryProvider asyncQueryProvider, IEntityType entityType)
        : base(asyncQueryProvider, entityType)
    {
    }

    /// <summary>
    /// </summary>
    protected override Expression VisitChildren(ExpressionVisitor visitor)
        => this;
}
