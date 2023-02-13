using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;

namespace  Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query.Internal;

/// <summary>
/// The default query SQL generator for Npgsql.
/// </summary>
public class NpgsqlTemporalQuerySqlGenerator : NpgsqlQuerySqlGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dependencies"></param>
    /// <param name="reverseNullOrderingEnabled"></param>
    /// <param name="postgresVersion"></param>
    public NpgsqlTemporalQuerySqlGenerator(
        QuerySqlGeneratorDependencies dependencies,
        bool reverseNullOrderingEnabled,
        Version postgresVersion)
        : base(dependencies, reverseNullOrderingEnabled, postgresVersion)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="innerJoinExpression"></param>
    /// <returns></returns>
    protected override Expression VisitInnerJoin(InnerJoinExpression innerJoinExpression)
    {
        // if the table is temporal

        Sql.Append("INNER JOIN ");
        Visit(innerJoinExpression.Table);
        Sql.Append(" ON ");
        Visit(innerJoinExpression.JoinPredicate);

        return innerJoinExpression;
    }
}
