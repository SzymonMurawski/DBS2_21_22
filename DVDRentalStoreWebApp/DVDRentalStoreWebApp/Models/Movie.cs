using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DVDRentalStoreWebApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }

        public Movie(int id, string title, int year, double price, List<Copy> copies = null)
        {
            Id = id;
            Title = title;
            Year = year;
            Price = price;
            Copies = copies;
        }
        public Movie()
        {

        }
    }
}
