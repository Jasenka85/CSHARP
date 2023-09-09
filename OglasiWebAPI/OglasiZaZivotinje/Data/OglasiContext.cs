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

    }
}
