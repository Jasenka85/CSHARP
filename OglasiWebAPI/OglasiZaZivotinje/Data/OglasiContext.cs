using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Models;

namespace OglasiZaZivotinje.Data
{
    public class OglasiContext: DbContext
    {
        public OglasiContext(DbContextOptions<OglasiContext> opcije) : base(opcije)
        {
        }
        public DbSet<Korisnik> Korisnik { get; set; }

        public DbSet<Oglas> Oglas { get; set; }

        public DbSet<Poruka> Poruka { get; set; }

        public DbSet<Crna_lista> Crna_lista { get; set; }

        public DbSet<Administrator> Administrator { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //oglas pripada jednom korisniku
            modelBuilder.Entity<Oglas>().HasOne(o => o.Korisnik);

            //poruka pripada jednom oglasu
            modelBuilder.Entity<Poruka>().HasOne(p => p.Oglas);

            //korisnik pripada jednoj crnoj listi
            modelBuilder.Entity<Crna_lista>().HasOne(c => c.Korisnik);
        }
    }
}
