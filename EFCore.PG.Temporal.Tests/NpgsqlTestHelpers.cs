using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Temporal.Tests
{
    public class NpgsqlTestHelpers : TestHelpers
    {
        protected NpgsqlTestHelpers() { }

        public static NpgsqlTestHelpers Instance { get; } = new();

        public override IServiceCollection AddProviderServices(IServiceCollection services)
            => services.AddEntityFrameworkNpgsql();

        public override DbContextOptionsBuilder UseProviderOptions(DbContextOptionsBuilder optionsBuilder)
            =>  optionsBuilder.UseNpgsql(new NpgsqlConnection("Host=localhost;Database=DummyDatabase"));
    }

}
