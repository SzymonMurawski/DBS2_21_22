using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ActiveRecord
{
    class MovieRecord
    {
        private static readonly string CONNECTION_STRING = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            ["ConnectionStrings:RentalDatabase"];
        public int ID { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public double Price { get; private set; }
        public List<CopyRecord> Copies { get; private set; }
        public MovieRecord(int id, string title, int year, double price, List<CopyRecord> copies = null)
        {
            ID = id;
            Title = title;
            Year = year;
            Price = price;
            Copies = copies;
        }

        public MovieRecord(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM movies WHERE movie_id = @ID", conn))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ID = (int)reader["movie_id"];
                        Title = (string)reader["title"];
                        Year = (int)reader["year"];
                        Price = Convert.ToDouble(reader["price"]);
                        Copies = CopyRecord.GetByMovieId(id);
                    } else
                    {
                        throw new ArgumentException($"Movie with id {id} cannot be found");
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"Movie {ID}: {Title} produced in {Year} costs {Price} and has {Copies.Count} copies";
        }

        public void Save()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                // This is an UPSERT operation - if record doesn't exist in the database it is created, otherwise it is updated
                using (var command = new NpgsqlCommand("INSERT INTO movies(movie_id, title, year, price) " +
                    "VALUES (@ID, @title, @year, @price) " +
                    "ON CONFLICT (movie_id) DO UPDATE " +
                    "SET title = @title, year = @year, price = @price", conn))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@title", Title);
                    command.Parameters.AddWithValue("@year", Year);
                    command.Parameters.AddWithValue("@price", Price);
                    command.ExecuteNonQuery();
                }
                // We need to save every copy in our list. 
                // Notice the "?" symbol - Copies might be an empty list, so we need protection from NullReferenceException
                Copies?.ForEach(obj => obj.Save());
            }
        }

        public void Remove()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                // If the movie has any copies this operation will fail
                using (var command = new NpgsqlCommand("DELETE FROM movies WHERE movie_id = @ID", conn))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ChangePrice(double new_price)
        {
            Price = new_price;
            Save();
        }
    }
}
