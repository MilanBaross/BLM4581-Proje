using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KampusPortali.Models;

namespace KampusPortali.Data
{
    public class ApplicationDbContext : IdentityDbContext<Kullanici, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Akademisyen> Akademisyenler { get; set; }
        public DbSet<Derslik> Derslikler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<KutuphaneMasasi> KutuphaneMasalari { get; set; }
        public DbSet<MasaRezervasyonu> MasaRezervasyonlari { get; set; }
        public DbSet<Etkinlik> Etkinlikler { get; set; }
        public DbSet<KayipEsya> KayipEsyalar { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<Mekan> Mekanlar { get; set; }
        public DbSet<Sinav> Sinavlar { get; set; }
        public DbSet<KullaniciDersi> KullaniciDersleri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kullanici>().ToTable("Kullanicilar");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roller");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("KullaniciRolleri");

            modelBuilder.Entity<Kullanici>()
                .HasIndex(k => k.OgrenciNo)
                .IsUnique();

            modelBuilder.Entity<Derslik>()
                .HasIndex(d => d.Ad)
                .IsUnique();

            modelBuilder.Entity<KutuphaneMasasi>()
                .HasIndex(m => m.MasaNo)
                .IsUnique();

            modelBuilder.Entity<Mekan>()
                .HasIndex(m => m.Ad)
                .IsUnique();

            modelBuilder.Entity<Mekan>()
                .Property(m => m.Tur)
                .HasConversion<string>()
                .HasMaxLength(30);

            modelBuilder.Entity<Mekan>()
                .Property(m => m.Id)
                .HasIdentityOptions(startValue: 100);

            modelBuilder.Entity<Mekan>()
                .HasData(MekanTohumVerisi.Mekanlar);

            modelBuilder.Entity<Derslik>()
                .HasOne(d => d.Mekan)
                .WithMany(m => m.Derslikler)
                .HasForeignKey(d => d.MekanId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Derslik>()
                .Property(d => d.Id)
                .HasIdentityOptions(startValue: 100);

            modelBuilder.Entity<Derslik>()
                .HasData(DersProgramiTohumVerisi.Derslikler);

            modelBuilder.Entity<Ders>()
                .HasIndex(d => d.Kod);

            modelBuilder.Entity<Ders>()
                .Property(d => d.Id)
                .HasIdentityOptions(startValue: 1000);

            modelBuilder.Entity<Ders>()
                .HasData(DersProgramiTohumVerisi.Dersler);

            modelBuilder.Entity<Sinav>()
                .Property(s => s.Tur)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<Sinav>()
                .HasIndex(s => s.DersKodu);

            modelBuilder.Entity<Sinav>()
                .HasOne(s => s.Derslik)
                .WithMany(d => d.Sinavlar)
                .HasForeignKey(s => s.DerslikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KullaniciDersi>()
                .HasIndex(k => new { k.KullaniciId, k.DersKodu })
                .IsUnique();

            modelBuilder.Entity<KullaniciDersi>()
                .HasOne(k => k.Kullanici)
                .WithMany()
                .HasForeignKey(k => k.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            // Derslik veya akademisyen silinirse bağlı dersler sessizce silinmesin.
            modelBuilder.Entity<Ders>()
                .HasOne(d => d.Derslik)
                .WithMany(d => d.Dersler)
                .HasForeignKey(d => d.DerslikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ders>()
                .HasOne(d => d.Akademisyen)
                .WithMany(a => a.Dersler)
                .HasForeignKey(d => d.AkademisyenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KutuphaneMasasi>()
                .Property(m => m.Id)
                .HasIdentityOptions(startValue: 100);

            modelBuilder.Entity<KutuphaneMasasi>()
                .HasData(KutuphaneTohumVerisi.Masalar);

            modelBuilder.Entity<MasaRezervasyonu>()
                .HasOne(r => r.Masa)
                .WithMany(m => m.Rezervasyonlar)
                .HasForeignKey(r => r.MasaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MasaRezervasyonu>()
                .HasOne(r => r.Kullanici)
                .WithMany(k => k.Rezervasyonlar)
                .HasForeignKey(r => r.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MasaRezervasyonu>()
                .HasIndex(r => new { r.MasaId, r.Baslangic, r.Bitis });

            modelBuilder.Entity<KayipEsya>()
                .HasOne(e => e.BildirenKullanici)
                .WithMany(k => k.BildirilenEsyalar)
                .HasForeignKey(e => e.BildirenKullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Duyuru>()
                .HasOne(d => d.Yazar)
                .WithMany(k => k.Duyurular)
                .HasForeignKey(d => d.YazarId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
