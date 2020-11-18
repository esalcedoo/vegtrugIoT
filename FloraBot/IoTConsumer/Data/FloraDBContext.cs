using Microsoft.EntityFrameworkCore;

namespace IoTConsumer.Data
{
    public class FloraDBContext : DbContext
    {
        public FloraDBContext(DbContextOptions<FloraDBContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<PlantEntity> Plants { get; set; }
        public DbSet<EntryEntity> Entries { get; set; }
        public DbSet<FloraDeviceEntity> FloraDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryEntity>()
                        .HasOne<PlantEntity>()
                        .WithMany()
                        .HasForeignKey(entry => entry.PlantId);

            modelBuilder.Entity<EntryEntity>()
                        .Property(entry => entry.Timestamp)
                        .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<EntryEntity>()
                        .HasIndex(entry => entry.Timestamp);

            modelBuilder.Entity<EntryEntity>()
                        .Property(entry => entry.Moisture)
                        .HasColumnType("smallint");

            modelBuilder.Entity<PlantEntity>()
                       .HasOne<FloraDeviceEntity>()
                       .WithOne(device => device.Plant)
                       .HasForeignKey<FloraDeviceEntity>(device => device.PlantId)
                       .IsRequired(false);

            modelBuilder.Entity<PlantEntity>()
                        .HasIndex(plant => plant.Name);

            modelBuilder.Entity<FloraDeviceEntity>()
                        .Property(device => device.Id)
                        .HasMaxLength(17);

            modelBuilder.Entity<FloraDeviceEntity>()
                        .Property(device => device.Battery)
                        .HasColumnType("smallint");
        }
    }
}
