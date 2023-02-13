using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;

using Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Extensions;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal
{
    public class MyContext26451 : DbContext
    {
        public MyContext26451(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<MainEntityDifferentTable> MainEntitiesDifferentTable { get; set; }
        public DbSet<MainEntitySameTable> MainEntitiesSameTable { get; set; }
        public DbSet<MainEntityMany> MainEntitiesMany { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainEntityDifferentTable>().ToTable(
                "MainEntityDifferentTable", tb => tb.IsTemporal(
                    ttb =>
                    {
                        ttb.HasPeriodStart("StartTime");
                        ttb.HasPeriodEnd("EndTime");
                       // ttb.UseHistoryTable("ConfHistory");
                    }));
            modelBuilder.Entity<MainEntityDifferentTable>().Property(me => me.Id).UseIdentityColumn();
            modelBuilder.Entity<MainEntityDifferentTable>().OwnsOne(me => me.OwnedEntity).WithOwner();
            modelBuilder.Entity<MainEntityDifferentTable>().OwnsOne(
                me => me.OwnedEntity, oe =>
                {
                    oe.ToTable(
                        "OwnedEntityDifferentTable", tb => tb.IsTemporal(
                            ttb =>
                            {
                                ttb.HasPeriodStart("StartTime");
                                ttb.HasPeriodEnd("EndTime");
                              //  ttb.UseHistoryTable("OwnedEntityHistory");
                            }));
                });

            modelBuilder.Entity<MainEntitySameTable>(
                eb =>
                {
                    eb.ToTable(
                        tb => tb.IsTemporal(
                            ttb =>
                            {
                                ttb.HasPeriodStart("StartTime").HasColumnName("StartTime");
                                ttb.HasPeriodEnd("EndTime").HasColumnName("EndTime");
                            }));

                    eb.OwnsOne(
                        x => x.OwnedEntity, oeb =>
                        {
                            oeb.WithOwner();
                            oeb.ToTable(
                                tb => tb.IsTemporal(
                                    ttb =>
                                    {
                                        ttb.HasPeriodStart("StartTime").HasColumnName("StartTime");
                                        ttb.HasPeriodEnd("EndTime").HasColumnName("EndTime");
                                    }));
                            oeb.OwnsOne(
                                x => x.Nested, neb =>
                                {
                                    neb.WithOwner();
                                    neb.ToTable(
                                        tb => tb.IsTemporal(
                                            ttb =>
                                            {
                                                ttb.HasPeriodStart("StartTime").HasColumnName("StartTime");
                                                ttb.HasPeriodEnd("EndTime").HasColumnName("EndTime");
                                            }));
                                });
                        });
                });

            modelBuilder.Entity<MainEntityMany>(
                eb =>
                {
                    eb.ToTable(tb => tb.IsTemporal());
                    eb.OwnsMany(x => x.OwnedCollection, oeb => oeb.ToTable(tb => tb.IsTemporal()));
                });
        }
    }

    public class MainEntityDifferentTable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public OwnedEntityDifferentTable OwnedEntity { get; set; }
    }

    public class OwnedEntityDifferentTable
    {
        public string Description { get; set; }
    }

    public class MainEntitySameTable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OwnedEntitySameTable OwnedEntity { get; set; }
    }

    public class OwnedEntitySameTable
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public OwnedEntitySameTableNested Nested { get; set; }
    }

    public class OwnedEntitySameTableNested
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public class MainEntityMany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OwnedEntityMany> OwnedCollection { get; set; }
    }

    public class OwnedEntityMany
    {
        public string Name { get; set; }
    }

    public class NpgsqlTemporalTableTest : NonSharedModelTestBase
    {
        protected override string StoreName
        => "TemporalTableSqlServerTest";

        protected TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;

        protected override ITestStoreFactory TestStoreFactory
            => NpgsqlNorthwindTestStoreFactory.Instance;

        protected void AssertSql(params string[] expected)
            => TestSqlLoggerFactory.AssertBaseline(expected);

        public virtual async Task Temporal_owned_basic(bool async)
        {
            var contextFactory = await InitializeAsync<MyContext26451>();
            using (var context = contextFactory.CreateContext())
            {
                var date = new DateTime(2000, 1, 1);

                var query = context.MainEntitiesDifferentTable.TemporalAsOf(date);
                var _ = async ? await query.ToListAsync() : query.ToList();
            }

            AssertSql(
    """
SELECT [m].[Id], [m].[Description], [m].[EndTime], [m].[StartTime], [o].[MainEntityDifferentTableId], [o].[Description], [o].[EndTime], [o].[StartTime]
FROM [MainEntityDifferentTable] FOR SYSTEM_TIME AS OF '2000-01-01T00:00:00.0000000' AS [m]
LEFT JOIN [OwnedEntityDifferentTable] FOR SYSTEM_TIME AS OF '2000-01-01T00:00:00.0000000' AS [o] ON [m].[Id] = [o].[MainEntityDifferentTableId]
""");
        }
    }
}
