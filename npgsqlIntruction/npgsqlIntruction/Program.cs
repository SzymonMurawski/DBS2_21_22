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

                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM movies WHERE year < 1990", conn))
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


                // react to user input
                Console.Write("Type movie title: ");
                string movieTitle = Console.ReadLine();

                // This is VERY BAD! DO not ever put something like this in the production code, use parameters instead.
                // Example of malicious title: Ronin' OR 1=1 OR title = 'asdf
                using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT title, price, movie_id AS id FROM movies WHERE title = '{movieTitle}'", conn))
                {
                    NpgsqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"Movie {dataReader[0]} has a price of {dataReader["price"]} and has id {dataReader["id"]}");
                    }
                    dataReader.Close();
                }

                
                // This is the good way of handling user input
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT title, price, movie_id AS id FROM movies WHERE title = @title", conn))
                {
                    cmd.Parameters.AddWithValue("@title", movieTitle);
                    NpgsqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"Movie {dataReader[0]} has a price of {dataReader["price"]} and has id {dataReader["id"]}");
                    }
                    dataReader.Close();
                }


                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    NpgsqlTransaction transaction = conn.BeginTransaction();
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;
                    try
                    {
                        cmd.CommandText = "INSERT INTO movies(movie_id, title, year) VALUES (102,'Ace Ventura 2', 1995)";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO movies(movie_id, title, year) VALUES (101,'Ace Ventura', 1994)";
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        Console.WriteLine("Both lines written to database");
                    } catch (Exception ex)
                    {
                        Console.WriteLine("Error occured, rolling back transaction");
                        Console.WriteLine(ex);
                        transaction.Rollback();
                    }
                }

                conn.Close();
            }

        }
    }
}
