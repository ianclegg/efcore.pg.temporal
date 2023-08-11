using Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Extensions.BuilderExtensions;

using Xunit;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Tests
{
    public class NpgsqlTableBuilderExtensionsTest
    {
        [Fact]
        public void can_set_table_as_temporal()
        {
            //var modelBuilder = CreateConventionModelBuilder();

            //modelBuilder.Entity<Customer>()
            //    .ToTable("customers",
            //        tb => tb.IsTemporal(
            //            ttb =>
            //            {
            //                ttb.HasPeriodStart("SystemTimeStart").HasColumnName("Start");
            //                ttb.HasPeriodEnd("SystemTimeEnd").HasColumnName("End");
            //            }
            //));


            //var entityType = modelBuilder.Model.FindEntityType(typeof(Customer));
        }

        protected virtual ModelBuilder CreateConventionModelBuilder()
            => NpgsqlTestHelpers.Instance.CreateConventionBuilder();

        private class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public IEnumerable<Order> Orders { get; set; }
        }

        private class Order
        {
            public int OrderId { get; set; }

            public int CustomerId { get; set; }
            public Customer Customer { get; set; }

            public OrderDetails Details { get; set; }
        }

        private class OrderDetails
        {
            public int Id { get; set; }

            public int OrderId { get; set; }
            public Order Order { get; set; }
        }
    }
}
