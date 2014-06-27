using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Training.DataStructures.Lib
{
    public static class DbExporter
    {
        public static void SaveLinkeListToDb<T>(LinkedList<T> list, String connectionString) 
            where T : IComparable<T>, IEquatable<T>
        {
            if (list == null) 
                throw new ArgumentNullException();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO Data(Data, Previous, Next) VALUES (@data, @previous, @next); select @@IDENTITY", connection);
                command.Parameters.Add(new SqlParameter("data", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("previous", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("next", SqlDbType.Int));

                int previousItemId = 0;
                int nextItemId = 0;

                connection.Open();
                foreach (var item in list)
                {
                    command.Parameters["data"].Value = item;
                    command.Parameters["previous"].Value = previousItemId;
                    command.Parameters["next"].Value = nextItemId;
                    previousItemId = (int)(decimal)command.ExecuteScalar();
                }
            }
        }

        //public static void ReadLinkedListFromDb(LinkedList<String> list)
        //{
        //    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
        //    {
        //        var dataSet = new DataSet();
        //        var adapter = new SqlDataAdapter("SELECT * FROM Data", connection);
        //        adapter.Fill(dataSet);

        //        foreach (var row in dataSet.Tables["Data"].Rows)
        //        {
                    
        //        }
        //    }
        //}

        public static void SaveToDbFromCollection<T>(ICollection<T> collection, String connectionString)
        {
            if (collection == null) 
                throw new ArgumentNullException();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO Data(data) VALUES (@data);", connection);
                command.Parameters.Add(new SqlParameter("data", SqlDbType.NVarChar));

                connection.Open();
                foreach (var item in collection)
                {
                    command.Parameters["data"].Value = item.ToString();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ReadFromDbIntoCollection<T>(ICollection<T> collection, String connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataSet = new DataSet();
                var adapter = new SqlDataAdapter("SELECT * FROM Data", connection);
                adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    collection.Add((T)row["Data"]);
                }
            }
        }
    }
}
