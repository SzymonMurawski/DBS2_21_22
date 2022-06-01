using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
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
        [NotMapped]
        public string Plot { get { return GetMoviePlot(); } }
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
        private string GetMoviePlot()
        {
            using var httpClient = new HttpClient();
            string url = $"http://www.omdbapi.com/?apikey=82dec9ee&t={Title}";
            var task = httpClient.GetAsync(url);
            task.Wait();
            var result = task.Result;
            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                var jsonString = content.Result;
                var parsedObject = JObject.Parse(jsonString);
                string plot = parsedObject["Plot"].ToString();
                return plot;
            }
            return "";
        }
    }
}
