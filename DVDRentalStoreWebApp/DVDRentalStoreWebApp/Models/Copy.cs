using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDRentalStoreWebApp.Models
{
    public class Copy
    {
        public int Id { get; set; }
        public bool Available { get; set; }
        public int MovieId { get; set; }
        public Copy(int id, bool available, int movieId)
        {
            Id = id;
            Available = available;
            MovieId = movieId;
        }
        public Copy()
        {

        }
    }
}
