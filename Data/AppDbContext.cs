using f1api.Models;
using Microsoft.EntityFrameworkCore;


namespace f1api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :DbContext(options)
    {
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Team> Teams => Set<Team>();

        public DbSet<Race> Races => Set<Race>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Drivers)
                .WithOne(d => d.Team)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.SetNull); // Takım silinirse pilotlar silinmez, takımsız kalır.

            modelBuilder.Entity<Race>()
                .HasOne(r => r.Winner)
                .WithMany()
                .HasForeignKey(r => r.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Race>()
                .HasOne(r => r.Second)
                .WithMany()
                .HasForeignKey(r => r.SecondId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Race>()
                .HasOne(r => r.Third)
                .WithMany()
                .HasForeignKey(r => r.ThirdId)
                .OnDelete(DeleteBehavior.Restrict);

            // ... Önceki Team ve İlişki ayarları burada kalacak ...

            // Teams verileri (Önceki adımda eklemiştik, burası referans olsun diye duruyor)
            // modelBuilder.Entity<Team>().HasData(...); 

            // --- DRIVER SEED DATA (2025 GÜNCEL KADROSU) ---
            /* modelBuilder.Entity<Driver>().HasData(
                 // 1. Red Bull Racing
                 new Driver { Id = 1, Name = "Max Verstappen", RacingNumber = 33, TeamId = 1 },
                 new Driver { Id = 2, Name = "Sergio Perez", RacingNumber = 11, TeamId = 1 },

                 // 2. Mercedes-AMG Petronas
                 new Driver { Id = 3, Name = "George Russell", RacingNumber = 63, TeamId = 2 },
                 new Driver { Id = 4, Name = "Kimi Antonelli", RacingNumber = 12, TeamId = 2 }, // Çaylak

                 // 3. Scuderia Ferrari
                 new Driver { Id = 5, Name = "Charles Leclerc", RacingNumber = 16, TeamId = 3 },
                 new Driver { Id = 6, Name = "Lewis Hamilton", RacingNumber = 44, TeamId = 3 }, // Ferrari Transferi

                 // 4. McLaren
                 new Driver { Id = 7, Name = "Lando Norris", RacingNumber = 4, TeamId = 4 },
                 new Driver { Id = 8, Name = "Oscar Piastri", RacingNumber = 81, TeamId = 4 },

                 // 5. Aston Martin
                 new Driver { Id = 9, Name = "Fernando Alonso", RacingNumber = 14, TeamId = 5 },
                 new Driver { Id = 10, Name = "Lance Stroll", RacingNumber = 18, TeamId = 5 },

                 // 6. Alpine
                 new Driver { Id = 11, Name = "Pierre Gasly", RacingNumber = 10, TeamId = 6 },
                 new Driver { Id = 12, Name = "Jack Doohan", RacingNumber = 7, TeamId = 6 }, // Çaylak

                 // 7. Williams Racing
                 new Driver { Id = 13, Name = "Alexander Albon", RacingNumber = 23, TeamId = 7 },
                 new Driver { Id = 14, Name = "Carlos Sainz", RacingNumber = 55, TeamId = 7 }, // Williams Transferi

                 // 8. Visa Cash App RB
                 new Driver { Id = 15, Name = "Yuki Tsunoda", RacingNumber = 22, TeamId = 8 },
                 new Driver { Id = 16, Name = "Liam Lawson", RacingNumber = 30, TeamId = 8 },

                 // 9. Kick Sauber
                 new Driver { Id = 17, Name = "Nico Hulkenberg", RacingNumber = 27, TeamId = 9 },
                 new Driver { Id = 18, Name = "GabrielBortoleto", RacingNumber = 5, TeamId = 9 }, // Çaylak

                 // 10. Haas F1 Team
                 new Driver { Id = 19, Name = "Esteban Ocon", RacingNumber = 31, TeamId = 10 },
                 new Driver { Id = 20, Name = "Oliver Bearman", RacingNumber = 87, TeamId = 10 } // Çaylak
             );
         */
        }
    }
}
