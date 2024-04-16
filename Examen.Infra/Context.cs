
using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;


namespace Examen.Infra
{
    public class Context : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Locataire> Locataires { get; set; }
        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Personne> Personnes { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        private readonly string _databaseName;

        public Context(string prenom = "yosr", string nom = "ben nagra")
        {
            _databaseName = $"Location{prenom}{nom}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={_databaseName};Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Locataire)
            .WithMany(l => l.Reservations)
            .HasForeignKey(r => r.LocataireKey);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Vehicule)
                .WithMany(v => v.Reservations)
                .HasForeignKey(r => r.VehiculeKey);
            modelBuilder.Entity<Reservation>()
                .HasKey(r => new { r.LocataireKey, r.VehiculeKey, r.DateReservation });
            modelBuilder.Entity<Locataire>()
                .HasDiscriminator<int>("TypeLocataire")
                .HasValue<Entreprise>(1)
                .HasValue<Personne>(2)
                .HasValue<Locataire>(0);
           
        


        }
    }
}