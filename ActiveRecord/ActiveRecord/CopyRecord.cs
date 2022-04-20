using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ActiveRecord
{
    class CopyRecord
    {
        private static readonly string CONNECTION_STRING =
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            ["ConnectionStrings:RentalDatabase"];
        public int ID { get; private set; }
        public bool Available { get; private set; }
        public int MovieId { get; private set; }
        public CopyRecord(int id, bool available, int movieId)
        {
            ID = id;
            Available = available;
            MovieId = movieId;
        }

        public CopyRecord(int id)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM copies WHERE copy_id = @ID", conn))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        
                        ID = id;
                        Available = (bool)reader["available"];
                        MovieId = (int)reader["movie_id"];
                    } else
                    {
                        throw new ArgumentException($"Copy with id {id} cannt be found");
                    }
                }
            }
        }

        public static List<CopyRecord> GetByMovieId(int movieId)
        {
            List<CopyRecord> list = new List<CopyRecord>();
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM copies WHERE movie_id = @movieID", conn))
                {
                    command.Parameters.AddWithValue("@movieID", movieId);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new CopyRecord((int)reader["copy_id"], (bool)reader["available"], (int)reader["movie_id"]));
                    }
                }
            }
            return list;
        }

        public void Save()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                // This is an UPSERT operation - if record doesn't exist in the database it is created, otherwise it is updated
                using (var command = new NpgsqlCommand("INSERT INTO copies(copy_id, available, movie_id) " +
                    "VALUES (@ID, @available, @movieId) " +
                    "ON CONFLICT (copy_id) DO UPDATE " +
                    "SET available = @available, movie_id = @movieId", conn))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@available", Available);
                    command.Parameters.AddWithValue("@movieId", MovieId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remove()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("DELETE FROM copies WHERE copy_id = @ID", conn))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
