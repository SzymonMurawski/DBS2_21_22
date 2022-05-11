using DVDRentalStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDRentalStoreWebApp.DAL
{
    public class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            context.Database.EnsureCreated();
            if (context.Movies.Any())
            {
                return;
            }

            var movies = new Movie[] {
                new Movie { Title = "Anchorman", Year = 2000, Price = 12},
                new Movie { Title = "Anchorman 2", Year = 2001, Price = 7},
                new Movie { Title = "Terminator", Year = 1993, Price = 22},
                new Movie { Title = "Jurassic Park", Year = 1999, Price = 29},
                new Movie { Title = "The Lord of the Rings", Year = 1997, Price = 11}
            };

            foreach (Movie m in movies)
            {
                context.Movies.Add(m);
            }
            context.SaveChanges();

            var copies = new Copy[] {
                new Copy{ MovieId = 1, Available = true},
                new Copy{ MovieId = 1, Available = true},
                new Copy{ MovieId = 1, Available = false},
                new Copy{ MovieId = 3, Available = true},
                new Copy{ MovieId = 3, Available = true},
                new Copy{ MovieId = 5, Available = true},
            };

            foreach (Copy c in copies)
            {
                context.Copies.Add(c);
            }
            context.SaveChanges();
        }
    }
}
