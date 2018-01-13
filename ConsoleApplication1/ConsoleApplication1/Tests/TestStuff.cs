using ConsoleApplication1.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Tests
{
    static class TestStuff
    {
        static public int createDatFilesFromSql(string connectionString, string fn)
        {
            int retval = 0;
            string query = string.Empty;

            query = File.ReadAllText(fn);
            //query = "SELECT TOP 5 [FirstName], [MiddleName], [LastName], [ModifiedDate] FROM [AdvWorks].[Person].[Person]";

            string fout = fn + ".ser.dat";
            Console.WriteLine("fn = {0} ==> {1}", fn, fout);
            
            DataTable dt = Dal.RunSqlCreateSerialData(connectionString, query);

            byte[] datOut = Helper.Serialize(dt);
            File.WriteAllBytes(fout, datOut);

            return retval;
        }
    }
}
