using System;
using System.Collections.Generic;
using System.Text;
using DataRepository;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MinionsClientApp
{
    class Queries
    {
        SqlConnection sqlConnection;
        public Queries()
        {
            IConfigurationBuilder conf = new ConfigurationBuilder();
            IConfigurationRoot root = conf.AddJsonFile("appsettings.json").Build();
            string connectionString = root.GetConnectionString("MinionsDB");
            sqlConnection = new SqlConnection(connectionString);
        }

        public void PrintAllMinionsByVillain()
        {
            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select v.Name, count(mv.MinionId) as Total " +
                              "from Villains v " +
                              "left join MinionsVillains mv on mv.VillainId = v.Id " +
                              "group by v.Name";

            cmd.Connection = sqlConnection;

            SqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("Name \t\t Total");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]} \t\t {reader["Total"]}");
            }

            sqlConnection.Close();
        }
            
        public void GetMinionsByVillainId(int id)
        {
            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Name from Villains where Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Connection = sqlConnection;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Villain: {reader["Name"]}");
                }
            }
            else
            {
                Console.WriteLine($"No villain with ID {id} exists in the database.");
                return;
            }
            

            reader.Close();

            //////next query///////

            cmd.CommandText = "select m.Id, m.Name, m.Age " +
                              "from Minions m " +
                              "join MinionsVillains mv on m.Id = mv.MinionId " +
                              "where mv.VillainId = @id";
            cmd.Connection = sqlConnection;
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Id"]}. \t {reader["Name"]} \t {reader["Age"]}");
                }
            }
            else
            {
                Console.WriteLine("(no minions)");
            }

            sqlConnection.Close();
        }
        
    }
}
