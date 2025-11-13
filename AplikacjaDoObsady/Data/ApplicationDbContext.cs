using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AplikacjaDoObsady.Models; // Dodaj ten using, aby uzyskać dostęp do swoich klas modeli

namespace AplikacjaDoObsady.Data
{
    // Zmieniono składnię konstruktora na tradycyjną, aby ułatwić dodanie OnModelCreating
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // --- DODANE WŁAŚCIWOŚCI DbSet DLA TWOICH MODELI ---
        public DbSet<Match> Matches { get; set; } = default!;
        public DbSet<Referee> Referee { get; set; } = default!;
        public DbSet<MatchAssignment> MatchAssignment { get; set; } = default!;

        // --- DODANA METODA OnModelCreating DLA KONFIGURACJI RELACJI ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WAŻNE: Zawsze wywołuj base.OnModelCreating(modelBuilder), ponieważ konfiguruje tabele Identity
            base.OnModelCreating(modelBuilder);

            // Konfiguracja relacji dla MatchAssignment, aby uniknąć problemu "multiple cascade paths"
            // i zapewnić prawidłowe mapowanie.

            // Relacja Match (MatchAssignment ma jeden Match)
            modelBuilder.Entity<MatchAssignment>()
                .HasOne(ma => ma.Match)
                .WithMany() // Zakładamy, że w klasie Match nie ma kolekcji MatchAssignments
                .HasForeignKey(ma => ma.MatchId)
                .OnDelete(DeleteBehavior.Cascade); // Usunięcie meczu usuwa jego przypisania (bezpieczne)

            // Relacja Sędzia Główny (MatchAssignment ma jednego MainReferee)
            modelBuilder.Entity<MatchAssignment>()
                .HasOne(ma => ma.MainReferee)
                .WithMany() // Zakładamy, że w klasie Referee nie ma kolekcji MatchAssignments od głównego sędziego
                .HasForeignKey(ma => ma.MainRefereeId) // Poprawna nazwa klucza obcego
                .OnDelete(DeleteBehavior.Restrict); // Nie można usunąć sędziego, jeśli jest MainReferee

            // Relacja Asystent I
            modelBuilder.Entity<MatchAssignment>()
                .HasOne(ma => ma.AssistantReferee1)
                .WithMany()
                .HasForeignKey(ma => ma.AssistantReferee1Id) // Poprawna nazwa klucza obcego
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Asystent II
            modelBuilder.Entity<MatchAssignment>()
                .HasOne(ma => ma.AssistantReferee2)
                .WithMany()
                .HasForeignKey(ma => ma.AssistantReferee2Id) // Poprawna nazwa klucza obcego
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Czwarty Sędzia (opcjonalny)
            modelBuilder.Entity<MatchAssignment>()
                .HasOne(ma => ma.FourthOfficial)
                .WithMany()
                .HasForeignKey(ma => ma.FourthOfficialId) // Poprawna nazwa klucza obcego
                .OnDelete(DeleteBehavior.SetNull); // SetNull, ponieważ FourthOfficialId jest nullable
        }
    }
}