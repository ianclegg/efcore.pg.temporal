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
    public class NpgsqlTemporalQueryTranslationPreprocessorFactory : IQueryTranslationPreprocessorFactory
    {
        private readonly INpgsqlSingletonOptions _npgsqlSingletonOptions;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public NpgsqlTemporalQueryTranslationPreprocessorFactory(
            QueryTranslationPreprocessorDependencies dependencies,
            RelationalQueryTranslationPreprocessorDependencies relationalDependencies,
            INpgsqlSingletonOptions npgsqlSingletonOptions)
        {
            Dependencies = dependencies;
            RelationalDependencies = relationalDependencies;
            _npgsqlSingletonOptions = npgsqlSingletonOptions;
        }

        /// <summary>
        ///     Dependencies for this service.
        /// </summary>
        protected virtual QueryTranslationPreprocessorDependencies Dependencies { get; }

        /// <summary>
        ///     Relational provider-specific dependencies for this service.
        /// </summary>
        protected virtual RelationalQueryTranslationPreprocessorDependencies RelationalDependencies { get; }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual QueryTranslationPreprocessor Create(QueryCompilationContext queryCompilationContext)
            => new NpgsqlTemporalQueryTranslationPreprocessor(Dependencies, RelationalDependencies, queryCompilationContext);
    }
}
