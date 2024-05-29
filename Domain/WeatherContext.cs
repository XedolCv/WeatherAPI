using Domain.Entitis;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class WeatherContext :DbContext
{
    public DbSet<Location> Locations { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    
    public WeatherContext(DbContextOptions options) :
        base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(property => property.Name == nameof(BaseEntity.CreateTime))
            .Select(property => modelBuilder.Entity(property.DeclaringEntityType.ClrType)
                .Property(nameof(BaseEntity.CreateTime))).ToList()
            .ForEach(builder => builder.HasDefaultValueSql("now()"));
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(property => property.Name == nameof(BaseEntity.Id))
            .Select(property => modelBuilder.Entity(property.DeclaringEntityType.ClrType)
                .Property(nameof(BaseEntity.Id))).ToList()
            .ForEach(builder => builder.HasDefaultValueSql("uuid_generate_v4()"));
    }
}