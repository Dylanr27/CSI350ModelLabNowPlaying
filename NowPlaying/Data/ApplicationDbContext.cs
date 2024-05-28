using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NowPlaying.Models;

namespace NowPlaying.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        //Seeds our DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
            .HasMany(e => e.Tickets)
            .WithOne(e => e.Movie)
            .HasForeignKey(e => e.MovieId)
            .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Alien", Description = "In deep space, the crew of the commercial starship Nostromo is awakened from their cryo-sleep capsules halfway through their journey home to investigate a distress call from an alien vessel.", Rating = "R", RunTime = "1h 57m", ImageURL = "https://upload.wikimedia.org/wikipedia/en/c/c3/Alien_movie_poster.jpg?20200710234615" },
                new Movie { Id = 2, Title = "Back to the Future", Description = "In this 1980s sci-fi classic, small-town California teen Marty McFly (Michael J. Fox) is thrown back into the '50s when an experiment by his eccentric scientist friend Doc Brown (Christopher Lloyd) goes awry.", Rating = "PG", RunTime = "1h 56m", ImageURL = "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg" },
                new Movie { Id = 3, Title = "Star Trek III: The Search for Spock", Description = "Adm. James T. Kirk (William Shatner) has defeated his archenemy but at great cost. His friend Spock has apparently been killed, the USS Enterprise is being scrapped, and starship physician Dr. Leonard \"Bones\" McCoy (DeForest Kelley) has taken ill. McCoy's odd behavior is evidence he's harboring Spock's katra, or animating spirit, and Kirk seeks to take the Enterprise back to the Genesis Planet and find his friend. Rebuffed, Kirk takes dramatic action that results in war with deadly Klingons.", Rating = "PG", RunTime = "1h 45m", ImageURL = "https://upload.wikimedia.org/wikipedia/en/b/b6/Star_Trek_III_The_Search_for_Spock.png" }


                );
        }
    }
}
