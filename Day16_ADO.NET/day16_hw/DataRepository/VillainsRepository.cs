using System;
using System.Collections.Generic;
using System.Text;
using DataModel;
using System.Data.SqlClient;

namespace DataRepository
{
    public class VillainsRepository : IRepository<Villains>
    {
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Villains> GetAll()
        {
            throw new NotImplementedException();
        }

        public Villains GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Villains obj)
        {
            SqlConnection sqlConnection = new SqlConnection(DbHelper.GetConnectionString());
            sqlConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Insert into Villains values (@id, @name, (SELECT Id from EvilnessFactors WHERE Name=@evilnessFactor))";
            cmd.Parameters.AddWithValue("@id", obj.Id);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@evilnessFactor", obj.EvilnessFactor);

            cmd.Connection = sqlConnection;
            int r = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return r;
        }

        public int Update(Villains obj)
        {
            throw new NotImplementedException();
        }
    }
}
