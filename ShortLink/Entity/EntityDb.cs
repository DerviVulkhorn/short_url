//NuGet -> Microsoft.EntityFrameworkCore.Tools
//Консоль диспетчера пакетов -> Add-Migration InitialDatabase -> Update-Database
using Microsoft.EntityFrameworkCore;
using ShortLink.Models;

namespace ShortLink.Entity
{
    public class EntityDb : DbContext
    {
        protected readonly IConfiguration Configuration;

        public EntityDb(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Users> users { get; set; }
        public DbSet<OriginalLink> originalLinks { get; set; }
        public DbSet<Code> codes { get; set; }

    }
}
