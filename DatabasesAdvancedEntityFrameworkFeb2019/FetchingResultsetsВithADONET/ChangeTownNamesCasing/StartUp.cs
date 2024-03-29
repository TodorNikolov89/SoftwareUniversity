﻿using InitialSetup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ChangeTownNamesCasing
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string updateTownNames = @"UPDATE Towns
   SET Name = UPPER(Name)
 WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

                using (SqlCommand command = new SqlCommand(updateTownNames, connection))
                {
                    command.Parameters.AddWithValue("@countryname", countryName);

                    int rowsAffected = (int)command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} town names were affected.");
                }

                string townNamesQuery = @" SELECT t.Name 
   FROM Towns as t
   JOIN Countries AS c ON c.Id = t.CountryCode
  WHERE c.Name = @countryName";

                List<string> towns = new List<string>();

                using (SqlCommand command = new SqlCommand(townNamesQuery, connection))
                {
                    command.Parameters.AddWithValue("@countryname", countryName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            towns.Add((string)reader[0]);
                        }
                    }
                }
                Console.WriteLine("["+string.Join(", ", towns)+"]");
            }
        }
    }
}
