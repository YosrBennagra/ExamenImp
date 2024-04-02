
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

        public Context(string prenom, string nom)
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
                .HasKey(r => new { r.LocataireId, r.VehiculeId, r.DateReservation });

            modelBuilder.Entity<Locataire>()
                .HasDiscriminator<int>("TypeLocataire")
                .HasValue<Personne>(0)
                .HasValue<Entreprise>(1);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Locataire)
                .WithMany(l => l.Reservations)
                .HasForeignKey(r => r.LocataireId)
                .HasPrincipalKey(l => l.Id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Vehicule)
                .WithMany(v => v.Reservations)
                .HasForeignKey(r => r.VehiculeId)
                .HasPrincipalKey(v => v.VehiculeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}