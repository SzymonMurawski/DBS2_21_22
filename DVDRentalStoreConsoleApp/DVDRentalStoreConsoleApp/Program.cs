using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DVDRentalStoreConsoleApp
{
    class Program
    {
        private static readonly string CONNECTION_STRING = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            ["ConnectionStrings:RentalDatabase"];
        static void Main(string[] args)
        {
            var menuElements = new ConsoleMenuElement[]
            {
                new ConsoleMenuElement("Show movies", ShowMoviesList),
                new ConsoleMenuElement("Show movie details", ShowMovieDetails),
                new ConsoleMenuElement("Add movie", AddMovie),
                new ConsoleMenuElement("Add copy", AddMovieCopy),
                new ConsoleMenuElement("Exit", Exit),
            };
            var consoleMenu = new ConsoleMenu(menuElements);
            consoleMenu.RunMenu();
        }

        static void ShowMoviesList()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING);
            conn.Open();
            using var command = new NpgsqlCommand("SELECT *, (SELECT COUNT(*) FROM copies WHERE copies.movie_id = movies.movie_id) AS total_copies, " +
                "(SELECT COUNT(*) FROM copies where copies.movie_id = movies.movie_id AND available = true) AS available_copies FROM movies ORDER BY movie_id", conn);
            NpgsqlDataReader reader = command.ExecuteReader();
            Console.WriteLine($"{new string(' ', 6)}ID |{new string(' ', 16)}Title |{new string(' ', 3)}Year | {new string(' ', 2)}Price | Copies (available/all)");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["movie_id"],8} | {reader["title"],20} | {reader["year"],6} | {reader["price"],6}$ | " +
                    $"{reader["available_copies"]}/{reader["total_copies"]}");
            }
            reader.Close();
        }

        static void ShowMovieDetails()
        {
            Console.Write("movie_id = ");
            try
            {
                int movieId = int.Parse(Console.ReadLine());
                using NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING);
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT *, (SELECT COUNT(*) FROM copies WHERE copies.movie_id = movies.movie_id) AS total_copies, " +
                "(SELECT COUNT(*) FROM copies where copies.movie_id = movies.movie_id AND available = true) AS available_copies FROM movies WHERE movie_id = @movie_id ORDER BY movie_id", conn))
                {
                    command.Parameters.AddWithValue("@movie_id", movieId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["movie_id"],8} | {reader["title"],20} | {reader["year"],6} | {reader["price"],6}$ | " +
                            $"{reader["available_copies"]}/{reader["total_copies"]}");
                    }
                    reader.Close();
                }
                using (var command = new NpgsqlCommand("SELECT a.* FROM actors a JOIN starring s ON s.actor_id = a.actor_id WHERE s.movie_id = @movie_id", conn))
                {
                    command.Parameters.AddWithValue("@movie_id", movieId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine($"Actors:\n{new string(' ', 15)} Name |{new string(' ', 10)}Birthday");
                    while (reader.Read())
                    {
                        string name = $"{reader["first_name"]} {reader["last_name"]}";
                        Console.WriteLine($"{name,20} | {reader["birthday"],10}");
                    }
                    reader.Close();
                }
                using (var command = new NpgsqlCommand("SELECT c.*, ( SELECT first_name || ' ' || last_name FROM clients cl JOIN rentals r ON r.client_id = cl.client_id WHERE r.copy_id = c.copy_id AND(r.date_of_return IS NULL OR r.date_of_return = (SELECT MAX(r2.date_of_return) FROM rentals r2 WHERE r2.copy_id = c.copy_id)) LIMIT 1) AS rent FROM copies c WHERE movie_id = @movie_id", conn))
                {
                    command.Parameters.AddWithValue("@movie_id", movieId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine($"Copies:\n copy_id | Available? | {new string(' ', 10)} Last rental");
                    while (reader.Read())
                    {

                        Console.WriteLine($"{reader["copy_id"],8} | {reader["available"],10} | {reader["rent"], 20}");
                    }
                    reader.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine("Error fetching movie details");
                Console.WriteLine(ex);
            }
        }

        static void AddMovie()
        {
            try
            {
                Console.Write("Title: ");
                string title = Console.ReadLine();
                Console.Write("Year: ");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Age restriction: ");
                int age = int.Parse(Console.ReadLine());
                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine());
                using NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING);
                conn.Open();
                using var command = new NpgsqlCommand("INSERT INTO movies (movie_id, title, year, age_restriction, price) " +
                    "VALUES ((SELECT MAX(movie_id) + 1 FROM movies), @title, @year, @age, @price)", conn);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@price", price);
                command.ExecuteNonQuery();
                Console.WriteLine("Movie succesfully added");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding movie");
                Console.WriteLine(ex);
            }
        }

        static void AddMovieCopy()
        {
            Console.Write("movie_id = ");
            try
            {
                int movieId = int.Parse(Console.ReadLine());
                using NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING);
                conn.Open();
                using var command = new NpgsqlCommand("INSERT INTO copies (copy_id, movie_id, available) " +
                    "VALUES ((SELECT MAX(copy_id) + 1 FROM copies), @movieId, true)", conn);
                command.Parameters.AddWithValue("@movieId", movieId);
                command.ExecuteNonQuery();
                Console.WriteLine("Copy succesfully added");
            } catch (Exception ex)
            {
                Console.WriteLine("Error adding movie copy");
                Console.WriteLine(ex);
            }
        }
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
