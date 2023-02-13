using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class TemporalAsOfQueryRootExpression : TemporalQueryRootExpression
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="pointInTime"></param>
        public TemporalAsOfQueryRootExpression(IEntityType entityType, DateTime pointInTime)
        : base(entityType)
        {
            PointInTime = pointInTime;
        }

        public TemporalAsOfQueryRootExpression(
           IAsyncQueryProvider queryProvider,
           IEntityType entityType,
           DateTime pointInTime)
       : base(queryProvider, entityType)
        {
            PointInTime = pointInTime;
        }

        public virtual DateTime PointInTime { get; }

        public override Expression DetachQueryProvider()
       => new TemporalAsOfQueryRootExpression(EntityType, PointInTime);

        public override EntityQueryRootExpression UpdateEntityType(IEntityType entityType)
    => entityType.ClrType != EntityType.ClrType
        || entityType.Name != EntityType.Name
            ? throw new InvalidOperationException(CoreStrings.QueryRootDifferentEntityType(entityType.DisplayName()))
            : new TemporalAsOfQueryRootExpression(entityType, PointInTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expressionPrinter"></param>
        protected override void Print(ExpressionPrinter expressionPrinter)
        {
            base.Print(expressionPrinter);
            expressionPrinter.Append($".TemporalAsOf({PointInTime})");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
      => obj != null
          && (ReferenceEquals(this, obj)
              || obj is TemporalAsOfQueryRootExpression queryRootExpression
              && Equals(queryRootExpression));

        private bool Equals(TemporalAsOfQueryRootExpression queryRootExpression)
            => base.Equals(queryRootExpression)
                && Equals(PointInTime, queryRootExpression.PointInTime);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(base.GetHashCode());
            hashCode.Add(PointInTime);

            return hashCode.ToHashCode();
        }
    }
}
