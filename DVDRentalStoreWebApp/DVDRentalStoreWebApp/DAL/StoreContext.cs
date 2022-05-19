using DVDRentalStoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DVDRentalStoreWebApp.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Employee>().ToTable("Employees");


            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 2, FirstName = "Bob", LastName = "Belcher", 
                    Birthday = DateTime.Parse("1977-01-23") },
                new Client { Id = 3, FirstName = "Hank", LastName = "Hill", 
                    Birthday = DateTime.Parse("1954-04-19") }
            );
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Jace", LastName = "Beleren", Location = "Poznan", 
                    HireDate = DateTime.Parse("2021-05-01") }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Anchorman", Year = 2000, Price = 12 },
                new Movie { Id = 2, Title = "Anchorman 2", Year = 2001, Price = 7 },
                new Movie { Id = 3, Title = "Terminator", Year = 1993, Price = 22 },
                new Movie { Id = 4, Title = "Jurassic Park", Year = 1999, Price = 29 },
                new Movie { Id = 5, Title = "The Lord of the Rings", Year = 1997, Price = 11 }
            );
            modelBuilder.Entity<Copy>().HasData(
                new Copy { Id = 1, MovieId = 1, Available = true },
                new Copy { Id = 2, MovieId = 1, Available = true },
                new Copy { Id = 3, MovieId = 1, Available = false },
                new Copy { Id = 4, MovieId = 3, Available = true },
                new Copy { Id = 5, MovieId = 3, Available = true },
                new Copy { Id = 6, MovieId = 5, Available = true }
            );
        }
    }
}
