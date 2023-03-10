using Amboosh_Pokemon_Review_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Amboosh_Pokemon_Review_Service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonCategory>()
            .HasKey(pc =>new {pc.PokemonId, pc.CategoryId});
        modelBuilder.Entity<PokemonCategory>()
            .HasOne(p => p.Pokemon)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(c => c.PokemonId)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<PokemonCategory>()
            .HasOne(p => p.Category)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<PokemonOwner>()
            .HasKey(po =>new {po.PokemonId, po.OwnerId});
        modelBuilder.Entity<PokemonOwner>()
            .HasOne(o => o.Pokemon)
            .WithMany(po => po.PokemonOwners)
            .HasForeignKey(bi => bi.PokemonId)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<PokemonOwner>()
            .HasOne(o => o.Owner)
            .WithMany(po => po.PokemonOwners)
            .HasForeignKey(bi => bi.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonCategory> PokemonCategories { get; set; }
    public DbSet<PokemonOwner> PokemonOwners { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
}