using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mobility.Types;
using Newtonsoft.Json;

namespace Mobility.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MemberData> Members { get; set; }

        public DbSet<TaxiData> Taxies { get; set; }

        public DbSet<CallData> Calls { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MemberData>(b =>
            {
                b.HasIndex(e => e.Email).IsUnique();

                b.Property(e => e.Type)
                    .HasConversion(new EnumToStringConverter<MemberType>());
            });

            modelBuilder.Entity<CallData>(b =>
            {
                b.Property(e => e.Position)
                    .HasConversion(
                        d => JsonConvert.SerializeObject(d, Formatting.None),
                        s => JsonConvert.DeserializeObject<PositionData>(s));

                b.Property(e => e.State)
                    .HasConversion(new EnumToStringConverter<CallState>());
            });

            modelBuilder.Entity<TaxiData>(b =>
            {
                b.Property(e => e.Position)
                    .HasConversion(
                        d => JsonConvert.SerializeObject(d, Formatting.None),
                        s => JsonConvert.DeserializeObject<PositionData>(s));
            });

            modelBuilder.HasAnnotation("ProductVersion", "3");
        }

    }
}
