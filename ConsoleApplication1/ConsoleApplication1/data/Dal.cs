using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Dal
    {
        static public DataTable RunSqlCreateSerialData(string connectionString, string query)
        {
            var retval = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(retval);
                conn.Close();
                da.Dispose();
            }
            return retval;
        }

        static public DataTable GetADataTable(string connectionString)
        {
            var retval = new DataTable();
            //string query = "SELECT TOP 2 * FROM[AdvWorks].[HumanResources].[Department]";
            string query = "SELECT TOP 5 [FirstName], [MiddleName], [LastName], [ModifiedDate] FROM [AdvWorks].[Person].[Person]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(retval);
                conn.Close();
                da.Dispose();
            }

            return retval;
        }

        public static void Main22(string connectionString)
        {
            //string connectionString =
            //    ConsoleApplication1.Properties.Settings.Default.ConnectionString;
            //
            // In a using statement, acquire the SqlConnection as a resource.
            //
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT TOP 2 *  FROM [AdvWorks].[HumanResources].[Department]", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    for (int col = 0; col < reader.FieldCount; col++)
                    {
                        Console.Write("[{0}] ", reader.GetName(col).ToString());
                        //Console.Write(reader.GetName(col).ToString());         // Gets the column name
                        //Console.Write(reader.GetFieldType(col).ToString());    // Gets the column type
                        //Console.Write(reader.GetDataTypeName(col).ToString()); // Gets the column database type
                    }
                    Console.WriteLine();

                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1} {2}",
                            reader.GetInt16(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
        }
    }
}
