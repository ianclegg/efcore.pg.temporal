namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class NpgsqlTemporalInjectionExpressionVisitor : ExpressionVisitor
    {
        /// <summary>
        /// 
        /// </summary>
        public NpgsqlTemporalInjectionExpressionVisitor()
        {
        }

       
        //protected override Expression VisitExtension(Expression extensionExpression)
        //    => extensionExpression switch
        //    {
        //        SelectExpression selectExpression => VisitSelect(selectExpression),
        //        _ => base.VisitExtension(extensionExpression)
        //    };

        //private Expression VisitSelect(SelectExpression selectExpression)
        //{
        //    foreach(var table in selectExpression.Tables)
        //    {
        //        if (table.FindAnnotation(NpgsqlAnnotationNames.TemporalOperationType) != null)
        //        {
        //            var newTable = (TableExpressionBase)Visit(table);
        //        }
                

        //    }
        //}
    }
}
