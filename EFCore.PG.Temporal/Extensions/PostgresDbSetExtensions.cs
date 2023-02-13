using Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Extensions;

public static class PostgresDbSetExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="utcPointInTime"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> TemporalAsOf<TEntity>(
        this DbSet<TEntity> source,
        DateTime utcPointInTime)
        where TEntity : class
    {
        var queryableSource = (IQueryable)source;
        var entityQueryRootExpression = (EntityQueryRootExpression)queryableSource.Expression;
        var entityType = entityQueryRootExpression.EntityType;

        return queryableSource.Provider.CreateQuery<TEntity>(
            new TemporalAsOfQueryRootExpression(
                entityQueryRootExpression.QueryProvider!,
                entityType,
                utcPointInTime)).AsNoTracking();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="utcFrom"></param>
    /// <param name="utcTo"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> TemporalFromTo<TEntity>(
    this DbSet<TEntity> source,
    DateTime utcFrom,
    DateTime utcTo)
    where TEntity : class
    {
        var queryableSource = (IQueryable)source;
        var entityQueryRootExpression = (EntityQueryRootExpression)queryableSource.Expression;
        var entityType = entityQueryRootExpression.EntityType;

        return queryableSource.Provider.CreateQuery<TEntity>(
            new TemporalFromToQueryRootExpression(
                entityQueryRootExpression.QueryProvider!,
                entityType,
                utcFrom,
                utcTo)).AsNoTracking();
    }

    //public static IQueryable<TEntity> TemporalBetween<TEntity>(
    //this DbSet<TEntity> source,
    //DateTime utcFrom,
    //DateTime utcTo)
    //where TEntity : class
    //{
    //    var queryableSource = (IQueryable)source;
    //    var entityQueryRootExpression = (EntityQueryRootExpression)queryableSource.Expression;
    //    var entityType = entityQueryRootExpression.EntityType;

    //    return queryableSource.Provider.CreateQuery<TEntity>(
    //        new TemporalBetweenQueryRootExpression(
    //            entityQueryRootExpression.QueryProvider!,
    //            entityType,
    //            utcFrom,
    //            utcTo)).AsNoTracking();
    //}

    //public static IQueryable<TEntity> TemporalContainedIn<TEntity>(
    //this DbSet<TEntity> source,
    //DateTime utcFrom,
    //DateTime utcTo)
    //where TEntity : class
    //{
    //    var queryableSource = (IQueryable)source;
    //    var entityQueryRootExpression = (EntityQueryRootExpression)queryableSource.Expression;
    //    var entityType = entityQueryRootExpression.EntityType;

    //    return queryableSource.Provider.CreateQuery<TEntity>(
    //        new TemporalContainedInQueryRootExpression(
    //            entityQueryRootExpression.QueryProvider!,
    //            entityType,
    //            utcFrom,
    //            utcTo)).AsNoTracking();
    //}
    //public static IQueryable<TEntity> TemporalAll<TEntity>(
    //this DbSet<TEntity> source)
    //where TEntity : class
    //{
    //    var queryableSource = (IQueryable)source;
    //    var entityQueryRootExpression = (EntityQueryRootExpression)queryableSource.Expression;
    //    var entityType = entityQueryRootExpression.EntityType;

    //    return queryableSource.Provider.CreateQuery<TEntity>(
    //        new TemporalAllQueryRootExpression(
    //            entityQueryRootExpression.QueryProvider!, entityType)).AsNoTracking();
    //}

}
