using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Internal;
using System.Diagnostics.CodeAnalysis;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query;

/// <summary>
/// 
/// </summary>
public class NpgsqlTemporalQueryableMethodTranslatingExpressionVisitor : NpgsqlQueryableMethodTranslatingExpressionVisitor
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dependencies"></param>
    /// <param name="relationalDependencies"></param>
    /// <param name="queryCompilationContext"></param>
    public NpgsqlTemporalQueryableMethodTranslatingExpressionVisitor(
        QueryableMethodTranslatingExpressionVisitorDependencies dependencies,
        RelationalQueryableMethodTranslatingExpressionVisitorDependencies relationalDependencies,
        QueryCompilationContext queryCompilationContext)
    : base(dependencies, relationalDependencies, queryCompilationContext)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="extensionExpression"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected override Expression VisitExtension(Expression extensionExpression)
    {
        if (extensionExpression is TemporalQueryRootExpression queryRootExpression)
        {
            var selectExpression = RelationalDependencies.SqlExpressionFactory.Select(queryRootExpression.EntityType);
            Func<TableExpression, TableExpressionBase> annotationApplyingFunc = queryRootExpression switch
            {
                //TemporalAllQueryRootExpression => te => te
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalOperationType, TemporalOperationType.All),
                TemporalAsOfQueryRootExpression asOf => te => te
                    .AddAnnotation(NpgsqlTemporalAnnotationNames.IsTemporal, TemporalOperationType.AsOf)
                    .AddAnnotation(NpgsqlTemporalAnnotationNames.TemporalAsOfPointInTime, asOf.PointInTime),
                //TemporalBetweenQueryRootExpression between => te => te
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalOperationType, TemporalOperationType.Between)
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalRangeOperationFrom, between.From)
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalRangeOperationTo, between.To),
                //TemporalContainedInQueryRootExpression containedIn => te => te
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalOperationType, TemporalOperationType.ContainedIn)
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalRangeOperationFrom, containedIn.From)
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalRangeOperationTo, containedIn.To),
                //TemporalFromToQueryRootExpression fromTo => te => te
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalOperationType, TemporalOperationType.FromTo)
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalRangeOperationFrom, fromTo.From)
                //    .AddAnnotation(SqlServerAnnotationNames.TemporalRangeOperationTo, fromTo.To),
                _ => throw new InvalidOperationException(queryRootExpression.Print()),
            };

            selectExpression = (SelectExpression)new TemporalAnnotationApplyingExpressionVisitor(annotationApplyingFunc)
               .Visit(selectExpression);

            return new ShapedQueryExpression(
                selectExpression,
                new RelationalEntityShaperExpression(
                    queryRootExpression.EntityType,
                    new ProjectionBindingExpression(
                        selectExpression,
                        new ProjectionMember(),
                        typeof(ValueBuffer)),
                    false));
        }

        return base.VisitExtension(extensionExpression);
    }

    private sealed class TemporalAnnotationApplyingExpressionVisitor : ExpressionVisitor
    {
        private readonly Func<TableExpression, TableExpressionBase> _annotationApplyingFunc;

        public TemporalAnnotationApplyingExpressionVisitor(Func<TableExpression, TableExpressionBase> annotationApplyingFunc)
        {
            _annotationApplyingFunc = annotationApplyingFunc;
        }

        [return: NotNullIfNotNull("expression")]
        public override Expression? Visit(Expression? expression)
            => expression is TableExpression tableExpression
                ? _annotationApplyingFunc(tableExpression)
                : base.Visit(expression);
    }
}

