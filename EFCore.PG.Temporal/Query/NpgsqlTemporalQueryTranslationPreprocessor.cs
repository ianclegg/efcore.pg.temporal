using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class NpgsqlTemporalQueryTranslationPreprocessor : RelationalQueryTranslationPreprocessor
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public NpgsqlTemporalQueryTranslationPreprocessor(
            QueryTranslationPreprocessorDependencies dependencies,
            RelationalQueryTranslationPreprocessorDependencies relationalDependencies,
            QueryCompilationContext queryCompilationContext)
            : base(dependencies, relationalDependencies, queryCompilationContext) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override Expression Process(Expression query)
        {
            var temporalQuery = new NpgsqlTemporalInjectionExpressionVisitor().Visit(query);
            return base.Process(temporalQuery);
        }
    }
}
