using eTickets.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                // 1. Cinemas
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema("Cinema 1", "/images/cinemas/32.jpeg", "This is the description of the first cinema"),
                        new Cinema("Cinema 2", "/images/cinemas/2.png", "This is the description of the second cinema"),
                        new Cinema("Cinema 3", "/images/cinemas/32.jpeg", "This is the description of the third cinema"),
                        new Cinema("Cinema 4", "/images/cinemas/222.png", "This is the description of the fourth cinema"),
                        new Cinema("Cinema 5", "/images/cinemas/qwww.jpeg", "This is the description of the fifth cinema")
                    });
                    context.SaveChanges();
                }

                // 2. Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor("Actor 1", "/images/actors/1.jpeg", "This is the Bio of the first actor"),
                        new Actor("Actor 2", "/images/actors/download (1).jpeg", "This is the Bio of the second actor"),
                        new Actor("Actor 3", "/images/actors/download (2).jpeg", "This is the Bio of the third actor"),
                        new Actor("Actor 4", "/images/actors/download (3).jpeg", "This is the Bio of the fourth actor"),
                        new Actor("Actor 5", "/images/actors/download (2).jpeg", "This is the Bio of the fifth actor")
                    });
                    context.SaveChanges();
                }

                // 3. Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer("Producer 1", "images/producers/images.jpeg", "This is the Bio of the first producer"),
                        new Producer("Producer 2", "images/producers/images.jpeg", "This is the Bio of the second producer"),
                        new Producer("Producer 3", "images/producers/Will-Packer-Producer.jpg", "This is the Bio of the third producer"),
                        new Producer("Producer 4", "images/producers/images.jpeg", "This is the Bio of the fourth producer"),
                        new Producer("Producer 5", "images/producers/Will-Packer-Producer.jpg", "This is the Bio of the fifth producer")
                    });
                    context.SaveChanges();
                }

                // 4. Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie("Life", "This is the Life movie description", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), 39.50, "/images/movies/life.jpg", MovieCategory.Documentary, 3, 3),
                        new Movie("The Shawshank Redemption", "This is the Shawshank Redemption description", DateTime.Now, DateTime.Now.AddDays(3), 29.50, "/images/movies/TheShawshankRedemption.jpg", MovieCategory.Action, 1, 1),
                        new Movie("Ghost", "This is the Ghost movie description", DateTime.Now, DateTime.Now.AddDays(7), 39.50, "/images/movies/Ghost.jpg", MovieCategory.Horror, 4, 4),
                        new Movie("Race", "This is the Race movie description", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-5), 39.50, "/images/movies/Race.jpg", MovieCategory.Documentary, 1, 2),
                        new Movie("Scoob", "This is the Scoob movie description", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-2), 39.50, "/images/movies/scoob.jpg", MovieCategory.Cartoon, 1, 3),
                        new Movie("Cold Soles", "This is the Cold Soles movie description", DateTime.Now.AddDays(3), DateTime.Now.AddDays(20), 39.50, "/images/movies/ColdSoles.jpg", MovieCategory.Drama, 1, 5)
                    });
                    context.SaveChanges();
                }

                // 5. Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie(1, 1),
                        new Actor_Movie(1, 3),
                        new Actor_Movie(2, 1),
                        new Actor_Movie(2, 4),
                        new Actor_Movie(3, 1),
                        new Actor_Movie(3, 2),
                        new Actor_Movie(3, 5),
                        new Actor_Movie(4, 2),
                        new Actor_Movie(4, 3),
                        new Actor_Movie(4, 4),
                        new Actor_Movie(5, 2),
                        new Actor_Movie(5, 3),
                        new Actor_Movie(5, 4),
                        new Actor_Movie(5, 5),
                        new Actor_Movie(6, 3),
                        new Actor_Movie(6, 4),
                        new Actor_Movie(6, 5)
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}