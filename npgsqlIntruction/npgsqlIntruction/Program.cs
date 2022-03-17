using System;
using Npgsql;

namespace npgsqlIntruction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=pwd;Database=rental;Port=5432"))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT title FROM movies", conn))
                {
                    NpgsqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"title: {dataReader[0]}");
                    }
                    dataReader.Close();
                }

                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM movies WHERE year < 1950", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT title, price, movie_id AS id FROM movies", conn))
                {
                    NpgsqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"Movie {dataReader[0]} has a price of {dataReader["price"]} and has id {dataReader["id"]}");
                    }
                    dataReader.Close();
                }

                conn.Close();
            }

        }
    }
}
