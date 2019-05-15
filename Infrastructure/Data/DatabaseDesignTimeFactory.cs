using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DatabaseDesignTimeFactory : IDesignTimeDbContextFactory<Database>
    {
        // This is copied straight from the 'API' project file
        // Not a good practice, but this class runs only in
        // development anyway
        private static readonly string SecretsId = "dac6b30c-63d4-44f8-9d1a-67b366e71626";

        public Database CreateDbContext(string[] args)
        {
            var options = new DbContextOptions<Database>();
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets(SecretsId)
                .Build();

            return new Database(options, configuration);
        }
    }
}
