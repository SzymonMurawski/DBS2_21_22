﻿using System;
using Npgsql;

namespace npgsqlIntruction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=pwd;Database=rental;Port=5432");
            
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT title FROM movies", conn);
            NpgsqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine($"title: {dataReader[0]}");
            }

            conn.Close();
        }
    }
}