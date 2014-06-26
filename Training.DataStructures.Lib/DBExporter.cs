using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Training.DataStructures.Lib
{
    public static class DBExporter
    {

        public static void SaveToDB(LinkedList<String> data)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                connection.Open();
                foreach (var d in data)
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Data(data) VALUES (@data); select @@IDENTITY";
                    //var param = command.CreateParameter();
                    SqlParameter p = new SqlParameter("data", d.ToString());
                    command.Parameters.Add(p);


                    var obj = command.ExecuteScalar();
                    //conn.Close();
                }

            }

        }

        public static void ReadFromDB(LinkedList<String> data)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM data", conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                    }
                }

                DataSet set = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM data", conn);
                //adapter.upFill(set);

                foreach (DataRow row in set.Tables[0].Rows)
                {
                    var x = (int)row["ID"];
                }
            }
        }
    }
}
