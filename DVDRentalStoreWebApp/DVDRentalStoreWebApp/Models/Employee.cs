using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDRentalStoreWebApp.Models
{
    public class Employee : Person
    {
        public string Location { get; set; }
        public DateTime HireDate { get; set; }
    }
}
