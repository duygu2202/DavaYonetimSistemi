using Microsoft.EntityFrameworkCore;
using DavaYonetimDB.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DavaYonetimDB.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Adliye> Adliyeler { get; set; }
        public DbSet<Sirket> Sirketler { get; set; }
        public DbSet<Sorumlu> Sorumlular { get; set; }
        public DbSet<DurumIcra> DurumIcralar { get; set; }
        public DbSet<DurumDava> DurumDavalar { get; set; }
        public DbSet<Dava> Davalar { get; set; }
        public DbSet<Icra> Icralar { get; set; }
        public DbSet<DavaSirket> DavaSirketler { get; set; }
        public DbSet<IcraSirket> IcraSirketler { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dava - Şirket ilişkisi (Many-to-Many)
            modelBuilder.Entity<DavaSirket>()
                .HasKey(ds => new { ds.DavaId, ds.SirketId });
                
            modelBuilder.Entity<DavaSirket>()
                .HasOne(ds => ds.Dava)
                .WithMany(d => d.DavaSirketleri)
                .HasForeignKey(ds => ds.DavaId);
                
            modelBuilder.Entity<DavaSirket>()
                .HasOne(ds => ds.Sirket)
                .WithMany(s => s.DavaSirketleri)
                .HasForeignKey(ds => ds.SirketId);
                
            // İcra - Şirket ilişkisi (Many-to-Many)
            modelBuilder.Entity<IcraSirket>()
                .HasKey(ıs => new { ıs.IcraId, ıs.SirketId });
                
            modelBuilder.Entity<IcraSirket>()
                .HasOne(ıs => ıs.Icra)
                .WithMany(i => i.IcraSirketleri)
                .HasForeignKey(ıs => ıs.IcraId);
                
            modelBuilder.Entity<IcraSirket>()
                .HasOne(ıs => ıs.Sirket)
                .WithMany(s => s.IcraSirketleri)
                .HasForeignKey(ıs => ıs.SirketId);
                
            base.OnModelCreating(modelBuilder);
        }
    }
} 