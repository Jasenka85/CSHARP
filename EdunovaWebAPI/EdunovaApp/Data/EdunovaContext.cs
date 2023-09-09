using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using EdunovaApp.Models;

namespace EdunovaApp.Data
{
    public class EdunovaContext : DbContext
    {
        // opcije se prvo prenose konstruktoru od DbContext
        public EdunovaContext(DbContextOptions<EdunovaContext> opcije) : base(opcije){ 
       
        
        }
        public DbSet<Smjer> Smjer{ get; set; }
    }
}
