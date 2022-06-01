using DVDRentalStoreWebApp.DAL;
using DVDRentalStoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DVDRentalStoreWebApp.Controllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesAPIController : ControllerBase
    {
        private readonly StoreContext _context;
        public MoviesAPIController(StoreContext context) // Dependency Injection
        {
            _context = context;
        }
        // GET: api/Movies
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _context.Movies;
        }

        // GET api/Movies/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Movies
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Movies/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
